using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Sentence : MonoBehaviour
{
    public AudioClip Clip;
    public UI_Tutorial _uiTutorial;

    public void Trigger()
    {
        Debug.Log("播放");
        AudioManager.GetInstance().PlaySound(Clip,_uiTutorial.gameObject.transform.position,false,50,  ()=>_uiTutorial.NextPage());
    }

    
}
