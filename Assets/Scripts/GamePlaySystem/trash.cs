using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using Random = UnityEngine.Random;

public class trash : MonoBehaviour
{
    public AudioClip AudioClip;
    public bool isCleaned = false;
    private Outline _outline;
    public float MaxSize = 1.2f;
    public float MinSize = 0.8f;
    public UnityEvent CapturedEvent;


    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        transform.localScale *= Random.Range(MinSize, MaxSize);
        Vector3 randomEulerAngles = new Vector3(
            Random.Range(0f, 360f),
            Random.Range(0f, 360f),
            Random.Range(0f, 360f)
        );
        transform.eulerAngles = randomEulerAngles;
        //TODO 增加声音Event 
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
        AudioManager.GetInstance().PlaySound(AudioClip,transform.position,false,50);
        transform.parent.GetComponent<WaterArea>().AddProgress();
        isCleaned = true;
        CapturedEvent.Invoke();
        gameObject.SetActive(false);
    }
    
    private IEnumerator HighLight(float sec)
    {
        _outline.enabled = true;
        yield return new WaitForSeconds(sec);
        _outline.enabled = false;
    }
}
