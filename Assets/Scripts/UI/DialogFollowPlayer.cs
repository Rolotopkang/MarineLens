using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFollowPlayer : MonoBehaviour
{
    public Transform playerCamera;  
    public GameObject dialogPanel;  

    public float distanceFromPlayer = 2f;  

    void Update()
    {
        if (dialogPanel.activeSelf)
        {
            dialogPanel.transform.position = playerCamera.position + playerCamera.forward * distanceFromPlayer;

            
            dialogPanel.transform.LookAt(playerCamera.position);
            dialogPanel.transform.Rotate(0, 180, 0);  
        }
    }
}
