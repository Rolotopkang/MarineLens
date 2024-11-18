using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFollowPlayer : MonoBehaviour
{
    private Transform playerCamera;  
    public GameObject dialogPanel;  

    public float distanceFromPlayer = 2f;

    private float FixHeight;
    private void Start()
    {
        playerCamera = playerManager.GetInstance().transform.GetChild(0).transform;
        FixHeight = Camera.main.transform.position.y;
        dialogPanel.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distanceFromPlayer;
        dialogPanel.transform.position = new Vector3(dialogPanel.transform.position.x, Camera.main.transform.position.y,
            dialogPanel.transform.position.z);
        dialogPanel.transform.LookAt(new Vector3(playerCamera.position.x,dialogPanel.transform.position.y,playerCamera.position.z));
        dialogPanel.transform.GetChild(0).Rotate(0,180,0);
    }


    void Update()
    {
        if (dialogPanel.activeSelf)
        {
            dialogPanel.transform.position = playerCamera.position - dialogPanel.transform.forward * distanceFromPlayer;
            dialogPanel.transform.position = new Vector3(dialogPanel.transform.position.x,FixHeight,
                dialogPanel.transform.position.z);

        }
    }
}
