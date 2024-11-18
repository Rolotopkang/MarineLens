using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFollowPlayer : MonoBehaviour
{
    private Transform playerCamera;  
    public GameObject dialogPanel;  

    public float distanceFromPlayer = 2f;
    public float SmoothTime = 0.1f;
    private Vector3 velocity;
    

    private float FixHeight;
    private void Start()
    {
        playerCamera = Camera.main.transform;
        dialogPanel.transform.GetChild(0).Rotate(0,180,0);
    }


    void Update()
    {
        // if (dialogPanel.activeSelf)
        // {
        //     Vector3 direction = -playerCamera.forward;
        //     direction.y = 0;
        //     if (direction == Vector3.zero) return;
        //     Quaternion targetRotation = Quaternion.LookRotation(direction);
        //     dialogPanel.transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        //     dialogPanel.transform.position = playerCamera.position - dialogPanel.transform.forward * distanceFromPlayer;
        //     dialogPanel.transform.position = new Vector3(dialogPanel.transform.position.x, Camera.main.transform.position.y,
        //         dialogPanel.transform.position.z);
        // }
        
        if (dialogPanel.activeSelf)
        {
            // 计算朝向
            Vector3 direction = -playerCamera.forward;
            direction.y = 0;
            if (direction == Vector3.zero) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            dialogPanel.transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

            // 目标位置计算
            Vector3 targetPosition = playerCamera.position - dialogPanel.transform.forward * distanceFromPlayer;
            targetPosition = new Vector3(targetPosition.x, Camera.main.transform.position.y, targetPosition.z);

            // 平滑移动到目标位置
            dialogPanel.transform.position = Vector3.SmoothDamp(dialogPanel.transform.position, targetPosition, ref velocity, SmoothTime);
        }
    }
}
