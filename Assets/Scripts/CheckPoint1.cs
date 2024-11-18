using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CheckPoint1 : CheckPoint
{
    public GameObject UIPrefab;
    private void Start()
    {
        Triggeraction += InstantUI;
    }

    private void InstantUI()
    {
        Instantiate(UIPrefab, Vector3.zero, quaternion.identity);
    }
}
