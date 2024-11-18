using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class WaterMoveProvider : MonoBehaviour
{
    public bool flying = true;
    public GameObject water;
    public GameObject PlayerHead;
    private void Start()
    {
        if (flying)
        {
            AutoHandPlayer.Instance.ToggleFlying();
        }
    }


    private void Update()
    {
        AutoHandPlayer.Instance.ToggleFlying(PlayerHead.transform.position.y < water.transform.position.y);
    }
}
