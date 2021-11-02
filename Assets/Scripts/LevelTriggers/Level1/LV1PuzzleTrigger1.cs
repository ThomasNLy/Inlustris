using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LV1PuzzleTrigger1 : MonoBehaviour
{
    [SerializeField]
    CutSceneCamera cutSceneCamera;

    [SerializeField]
    LV1CameraEndSequence cameraSequence;

    [SerializeField]
    Image cutSceneWhite;

    Transform starsCollected;
    StarCounter p;
    StarsConnectedCounter starsConnectedCounter;
    bool actionButtonPressed;
    bool movingStarsTriggered;
    bool cameraTriggered;
    bool phoenixTriggered;
    GameObject playerUI;
    float delayFadeToWhite = 6.0f;

    GameObject phoenixRef;

    private void Start()
    {
        actionButtonPressed = false;
        starsCollected = GameObject.FindGameObjectWithTag("StarsCollected").transform;
        p = GameObject.FindGameObjectWithTag("StarCounter").GetComponent<StarCounter>();
        starsConnectedCounter = GameObject.FindGameObjectWithTag("StarCounter").GetComponent<StarsConnectedCounter>();
        playerUI = GameObject.FindGameObjectWithTag("UI");
    }

    private void Update()
    {
        if (p.Amount >= 8)
        {
            StartCameraSequence();
        }

        if (starsConnectedCounter.Amount >= 8 && !phoenixTriggered)
        {
            phoenixTriggered = true;
            StartCoroutine("SummonPhoenix");
        }
    }



    public void StartCameraSequence()
    {
        if (!cameraTriggered)
        {
            cameraTriggered = true;
            cameraSequence.activated = true;

            foreach (Transform star in starsCollected)
            {
                star.GetComponent<StarPickedUp>().movementTriggered = true;
                star.GetComponent<StarPickedUp>().lineMovementTriggered = true;
            }
            StartCoroutine("WhiteToFade", 0.0f);
            cutSceneWhite.GetComponentInParent<ScreenWhiteToFade>().PlayFadeSound();
            cutSceneCamera.ActivateCutSceneCamera();
        }
    }

    IEnumerator SummonPhoenix()
    {
        yield return null;
        Instantiate((GameObject)Resources.Load("prefabs/level1/PhoenixEmblem", typeof(GameObject)), cameraSequence.target.position, Quaternion.identity);
        yield return new WaitForSeconds(3.0f);
        phoenixRef = Instantiate((GameObject)Resources.Load("prefabs/phoenixAnimated", typeof(GameObject)), cameraSequence.target.position, Quaternion.Euler(-80.0f, 0, 0));
        cameraSequence.target = phoenixRef.transform;
        cameraSequence.phoenixSummoned = true;
        StartCoroutine("FadeToWhite", delayFadeToWhite);
    }

    IEnumerator WhiteToFade(float delay)
    {
        yield return new WaitForSeconds(delay);
        Color temp = cutSceneWhite.color;
        temp.a = 1;
        while (true)
        {
            temp.a -= 1 / 255f;
            cutSceneWhite.color = temp;
            if (temp.a <= 0)
            {
                temp.a = 0;
                cutSceneWhite.color = temp;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    IEnumerator FadeToWhite(float delay)
    {
        yield return new WaitForSeconds(delay);
        Color temp = cutSceneWhite.color;
        temp.a = 0;
        while (true)
        {
            temp.a += 1 / 255f;
            cutSceneWhite.color = temp;
            if (temp.a >= 1)
            {
                temp.a = 1;
                cutSceneWhite.color = temp;
                break;
            }
            yield return new WaitForSeconds(0.001f);
        }
        cutSceneCamera.DisableCutSceneCamera();
        SceneManager.LoadScene("menu");
    }
}
