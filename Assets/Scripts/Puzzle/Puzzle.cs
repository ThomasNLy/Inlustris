using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public StarVertex thisStarVertex;
    public bool puzzleCompleted;
    public GameObject puzzleFinishedIndicator;
}
