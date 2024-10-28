using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class trash : MonoBehaviour
{

    public bool isCleaned = false;
    private Outline _outline;


    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void HighLightForSec(float sec)
    {
        
    }

    public void TrashCaptured()
    {
        if (isCleaned)
        {
            return;
        }
        transform.parent.GetComponent<WaterArea>().AddProgress();
        isCleaned = true;
        gameObject.SetActive(false);
    }
    
    private IEnumerator HighLight(float sec)
    {
        _outline.enabled = true;
        yield return new WaitForSeconds(sec);
        _outline.enabled = false;
    }
}
