using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public float mouseSens; //mouse sensitivity
    public float dstFromTarget;
    //public float cameraLeftOffset;
    //public float cameraRightOffset;
    public Transform tempLeftRef;
    public Transform tempRightRef;
    public Transform tempLookRef;

    public Transform wallDetectRef;
    public Transform target; //the target the camera follows
    public Vector2 pitchMinMax; //pitch range values in the form of a vector2
    public Vector2 scrollMinMax;
    //public Light minimapLight;

    int raycastLayerMask = 8;

    public float currentRaySmoothVel;
    public float rotationSmoothTime;
    Vector3 rotationSmoothVel;
    Vector3 currentRotation;

    public float rayToTargetDistance;

    public float minClampOffset = 0.5f;
    public RaycastHit collisionInBetween;
    public RaycastHit leftCollision;
    public RaycastHit rightCollision;
    bool collisionInBetweenExists;
    bool leftCollisionExists;
    bool rightCollisionExists;

    float yaw; //horizontal camera movement
    float pitch; //vertical camera movement
    float scrollFactor;
    float startClipPlaneValue;

    float lookRotationSpeed = 5.0f;

    bool coroutineLooking;

    private bool mouseCameraRotationDisabled;
    public bool MouseCameraRotationDisabled
    {
        get { return mouseCameraRotationDisabled; }
        set { mouseCameraRotationDisabled = value; }
    }

    void Start()
    {
        scrollFactor = 0.2f;
        scrollMinMax = new Vector2(20, 30);
        mouseSens = 3.5f;
        pitchMinMax = new Vector2(-60, 85);
        rotationSmoothTime = 0.12f;
        startClipPlaneValue = 2.0f;
        dstFromTarget = scrollMinMax.x;
        rayToTargetDistance = dstFromTarget;
        transform.position = target.position - transform.forward * rayToTargetDistance;
    }

    void Update()
    {
        if (!MouseCameraRotationDisabled)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSens; //moves camera horizontally using mouse leftright
            pitch -= Input.GetAxis("Mouse Y") * mouseSens; //moves camera vertically using mouse updown
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y); //set the pitch range limit

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVel, rotationSmoothTime);
            transform.eulerAngles = currentRotation;

            ScrollForDistance();
            //rayToTargetDistance = dstFromTarget; //resets
            if (collisionInBetweenExists && collisionInBetween.collider.CompareTag("Platform"))
            {
                //rayToTargetDistance = (collisionInBetween.point - target.position).magnitude;
                rayToTargetDistance = Mathf.SmoothDamp(rayToTargetDistance, (collisionInBetween.point - target.position).magnitude, ref currentRaySmoothVel, 0.5f, 20.0f);
            }

            else if ((leftCollisionExists && leftCollision.collider.CompareTag("Platform")) || (rightCollisionExists && rightCollision.collider.CompareTag("Platform")))
            {
                rayToTargetDistance = Mathf.SmoothDamp(rayToTargetDistance, 5.0f, ref currentRaySmoothVel, 0.05f, 20.0f);
            }

            else
            {
                rayToTargetDistance = Mathf.SmoothDamp(rayToTargetDistance, scrollMinMax.x, ref currentRaySmoothVel, 0.05f, 20.0f);
            }


            rayToTargetDistance = Mathf.Clamp(rayToTargetDistance, 0.0f, scrollMinMax.x); //but camera stops
            transform.position = target.position - transform.forward * rayToTargetDistance;
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetToCamera = transform.position - target.position;
        if (!leftCollisionExists && !rightCollisionExists)
            collisionInBetweenExists = Physics.Raycast(target.position, targetToCamera, out collisionInBetween, targetToCamera.magnitude + 5.0f);
        if (!collisionInBetweenExists && !rightCollisionExists)
            leftCollisionExists = Physics.Raycast(wallDetectRef.position, -transform.right, out leftCollision, 8.0f);
        if (!leftCollisionExists && !collisionInBetweenExists)
            rightCollisionExists = Physics.Raycast(wallDetectRef.position, transform.right, out rightCollision, 8.0f);
    }

    void ScrollForDistance()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            dstFromTarget -= scrollFactor;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            dstFromTarget += scrollFactor;
        }
    }

    public IEnumerator LookAtTarget(Transform currentTarget, float time)
    {
        MouseCameraRotationDisabled = true;
        if (MouseCameraRotationDisabled && !coroutineLooking)
        {
            coroutineLooking = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementPlayer>().MovementOff = true;
            StartCoroutine(LookAtTargetLoop(currentTarget));
            StartCoroutine(LookAtTargetEnd(time));
        }
        yield return null;
    }

    public IEnumerator LookAtTarget(Transform currentTarget)
    {
        MouseCameraRotationDisabled = true;
        if (MouseCameraRotationDisabled && !coroutineLooking)
        {
            coroutineLooking = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementPlayer>().MovementOff = true;
            while (coroutineLooking)
            {
                Debug.Log(currentTarget == null);
                if (currentTarget == null)
                    yield break;
                Vector3 targetDir = (currentTarget.position - transform.position).normalized;
                Debug.Log(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * lookRotationSpeed));
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * lookRotationSpeed);
                yield return null;
            }
        }
        StartCoroutine(LookAtTargetEnd());
    }

    private IEnumerator LookAtTargetLoop(Transform currentTarget)
    {
        while (coroutineLooking)
        {
            Vector3 targetDir = (currentTarget.position - transform.position).normalized;
            Debug.Log(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * lookRotationSpeed));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * lookRotationSpeed);
            yield return null;
        }
    }

    private IEnumerator LookAtTargetEnd(float time)
    {
        yield return new WaitForSeconds(time);
        coroutineLooking = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementPlayer>().MovementOff = false;
        MouseCameraRotationDisabled = false;
    }

    public IEnumerator LookAtTargetEnd()
    {
        coroutineLooking = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<MovementPlayer>().MovementOff = false;
        MouseCameraRotationDisabled = false;
        yield return null;
    }
}
