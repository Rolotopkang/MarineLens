using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class trash : MonoBehaviour
{

    private Outline _outline;


    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void HighLightForSec(float sec)
    {
        
    }
    
    //TODO 进度
    
    public void TrashCaptured()
    {
        Destroy(gameObject);
    }
    
    private IEnumerator HighLight(float sec)
    {
        _outline.enabled = true;
        yield return new WaitForSeconds(sec);
        _outline.enabled = false;
    }
}
