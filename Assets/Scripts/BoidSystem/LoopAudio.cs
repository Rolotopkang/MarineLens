using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip audioClip; // 指定要播放的音频片段
    public float volume = 10.0f; // 音量大小，范围0.0到1.0

    private AudioSource audioSource;

    void Start()
    {
        // 创建一个新的AudioSource组件
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;

        // 设置为3D音效
        audioSource.spatialBlend = 1.0f;

        // 设置为循环播放
        audioSource.loop = true;

        // 播放音频
        audioSource.Play();
    }
}
