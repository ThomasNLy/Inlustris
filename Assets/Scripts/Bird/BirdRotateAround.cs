using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdRotateAround : RotateAround
{
    private void Awake()
    {
        if (target == null)
            target = transform;
    }
}
