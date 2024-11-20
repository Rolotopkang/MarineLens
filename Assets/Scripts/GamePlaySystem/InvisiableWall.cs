using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiableWall : MonoBehaviour
{
    public float tmp_coolDown;
    public float coolDown = 5f;
    public bool inCoolDown = false;
    public GameObject WarningUI;
    private GameObject currentwarningUI;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerWarningUI();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerWarningUI();
        }
    }

    private void Update()
    {
        if (inCoolDown)
        {
            tmp_coolDown -= Time.deltaTime;
            if (tmp_coolDown <= 0)
            {
                tmp_coolDown = 0;
                inCoolDown = false;
                DestoryWarningUI();
            }
        }
    }
    
    

    private void TriggerWarningUI()
    {
        if (!inCoolDown)
        {
            inCoolDown = true;
            tmp_coolDown = coolDown;
            currentwarningUI = Instantiate(WarningUI);
            Debug.Log("开始倒计时");
        }
    }

    private void DestoryWarningUI()
    {
        if (currentwarningUI != null) 
        {
            Destroy(currentwarningUI); 
            currentwarningUI = null;  
        }


        Debug.Log("倒计时结束");
    }
    
    
}
