using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Audio : MonoBehaviour
{
    public AudioClip AudioClip;

    public void Play()
    {
        AudioManager.GetInstance().PlaySound(AudioClip,new Vector3(-250,22,-122), false,100,() => {
            Debug.Log("播放结束");});
    }
}
