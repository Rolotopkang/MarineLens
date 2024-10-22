using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ChangeGravity : MonoBehaviour
{
    private DynamicMoveProvider DynamicMoveProvider;
    private UpDownMovement UpDownMovement;
    public GameObject water;
    public GameObject PlayerHead;

    private void Start()
    {
        DynamicMoveProvider = GetComponent<DynamicMoveProvider>();
        UpDownMovement = GetComponent<UpDownMovement>();
    }

    private void Update()
    {
        DynamicMoveProvider.useGravity = PlayerHead.transform.position.y >= water.transform.position.y;
        DynamicMoveProvider.enableFly = !(PlayerHead.transform.position.y >= water.transform.position.y);
        UpDownMovement.canMove = !(PlayerHead.transform.position.y >= water.transform.position.y);
    }
}
