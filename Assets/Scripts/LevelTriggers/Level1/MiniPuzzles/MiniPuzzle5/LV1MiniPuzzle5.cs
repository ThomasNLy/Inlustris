using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV1MiniPuzzle5 : Puzzle
{
    public int timeCounter;
    public int amountSelected;
    public int amountOfCirclesPassed;
    public int amountOfCirclesCorrectlyPassed;
    public TriggerLights lightSwitch;
    public SpawnAssets assetSpawnSwitch;
    public AudioSource puzzleFinishedSound;

    void Start()
    {
        puzzleFinishedSound.Play();
        StartCoroutine("CircleSequence");
    }

    public void ResetTimer()
    {
        timeCounter = 0;
    }

    public void ResetCounters()
    {
        amountSelected = 0;
        amountOfCirclesPassed = 0;
        amountOfCirclesCorrectlyPassed = 0;
    }

    IEnumerator CircleSequence()
    {
        PickRandomCircles();
        while (!puzzleCompleted)
        {
            yield return new WaitForSeconds(1.0f);
            timeCounter++;
            if ((amountOfCirclesPassed == amountOfCirclesCorrectlyPassed) && (amountOfCirclesCorrectlyPassed == amountSelected))
            {
                puzzleCompleted = true;
                puzzleFinishedSound.Play();
                IndicatePuzzleFinished();
                GameObject indicatorRef = Instantiate(puzzleFinishedIndicator, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
                indicatorRef.GetComponent<PuzzleFinishedIndicator>().target = thisStarVertex.starRef.transform;
                indicatorRef.GetComponent<PuzzleFinishedIndicator>().lightSwitch = this.lightSwitch;
                indicatorRef.GetComponent<PuzzleFinishedIndicator>().assetSpawnSwitch = this.assetSpawnSwitch;
                indicatorRef.GetComponent<PuzzleFinishedIndicator>().thisStarVertex = this.thisStarVertex;
                indicatorRef.GetComponent<PuzzleFinishedIndicator>().starCharge = thisStarVertex.starRef.GetComponent<StarPickedUp>().charge;
                indicatorRef.GetComponent<PuzzleFinishedIndicator>().fullyChargedSound = thisStarVertex.starRef.GetComponent<StarPickedUp>().fullyChargedSound;
            }
            if (timeCounter == 8)
            {
                ResetCounters();
                ResetTimer();
                ClearCircles();
                yield return new WaitForSeconds(1.0f);
                PickRandomCircles();
            }
        }
    }

    public void PickRandomCircles()
    {
        foreach (Transform circleT in transform)
        {
            int randomInt = Random.Range(0, 6);
            bool selectBool = false;
            if (randomInt % 2 == 0)
                selectBool = true;
            circleT.GetComponent<LV1MiniPuzzle5Circle>().selected = selectBool;
            if (selectBool)
            {
                Color colorRef = circleT.GetComponent<LV1MiniPuzzle5Circle>().thisRenderer.material.color;
                colorRef.g = 0;
                circleT.GetComponent<LV1MiniPuzzle5Circle>().thisRenderer.material.color = colorRef;
                amountSelected++;
            }
        }
    }

    public void ClearCircles()
    {
        foreach (Transform circleT in transform)
        {
            circleT.GetComponent<LV1MiniPuzzle5Circle>().traversedThrough = false; ;
            circleT.GetComponent<LV1MiniPuzzle5Circle>().selected = false;
            Color colorRef = circleT.GetComponent<LV1MiniPuzzle5Circle>().thisRenderer.material.color;
            colorRef = Color.white;
            circleT.GetComponent<LV1MiniPuzzle5Circle>().thisRenderer.material.color = colorRef;
        }
    }

    public void IndicatePuzzleFinished()
    {
        foreach (Transform circleT in transform)
        {
            Color colorRef = circleT.GetComponent<LV1MiniPuzzle5Circle>().thisRenderer.material.color;
            colorRef.r = 0.5f;
            colorRef.g = 0.5f;
            circleT.GetComponent<LV1MiniPuzzle5Circle>().thisRenderer.material.color = colorRef;
        }
    }
}
