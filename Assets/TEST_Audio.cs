using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Audio : MonoBehaviour
{
    public AudioClip AudioClip;

    public void Play()
    {
        AudioManager.GetInstance().PlaySound(AudioClip,Vector3.zero, false,5000,() => {
            Debug.Log("播放结束");});
    }
}
