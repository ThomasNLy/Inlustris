using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour {

    public float walkSpeed;
    public float runSpeed;
    public float turnSmoothTime = 0.2f;
    public float turnSmoothVelocity = 0.1f;

    public float speedSmoothTime;
    float speedSmoothVelocity;
    float currentSpeed;
    Transform cameraTransform;
    bool isGrounded;
    public ParticleSystem charge;
    private bool movementOff;
    public bool MovementOff
    {
        get { return movementOff; }
        set { movementOff = value; }
    }

    Rigidbody playerRB;

    // Use this for initialization
    void Start () {
        cameraTransform = Camera.main.transform;
        playerRB = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
    }

    void Movement() {
        if (!movementOff)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //horizontal input(A,D) vertical input(W,S)

            Vector2 inputDir = input.normalized; //convert to unit vector

            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y; //angle formed by the two input variables
            float yRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);

            transform.eulerAngles = new Vector3(cameraTransform.eulerAngles.x, yRotation, 0); //turn FROM player's current y-angle TO the target's y-angle

            bool running = Input.GetKey(KeyCode.LeftShift); //boolean to detect if leftshiftkey is pressed
            float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude; //if leftshiftkey is pressed, speed = runspeed. else, speed = walkspeed
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
            playerRB.velocity = transform.forward * currentSpeed; //add translation to character so that it actually moves
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Platform"))
            GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
