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
    private GameObject currentWarningUI;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

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

    public void DelSelf()
    {
        BoxCollider[] tmp_C = GetComponents<BoxCollider>();
        foreach (BoxCollider boxCollider in tmp_C)
        {
            boxCollider.enabled = false;
        }
    }

    private void TriggerWarningUI()
    {
        if (!inCoolDown)
        {
            inCoolDown = true;
            tmp_coolDown = coolDown;
            currentWarningUI = Instantiate(WarningUI);
            
            Debug.Log("开始倒计时");
        }
    }

    private void DestoryWarningUI()
    {
        Destroy(currentWarningUI);
        Debug.Log("倒计时结束");
    }
    
    
}
