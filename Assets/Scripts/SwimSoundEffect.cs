using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimSoundEffect : MonoBehaviour
{
    public AudioClip[] swimSounds;
    private AudioSource audioSource;

    void Start()
    {
        // 获取AudioSource组件
        audioSource = GetComponent<AudioSource>();

        // 如果没有AudioSource组件，则自动添加一个
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 调用此方法时播放随机音效
    public void PlaySwimSound()
    {
        if (swimSounds.Length > 0)
        {

            // 随机选择一个音效
            int randomIndex = Random.Range(0, swimSounds.Length);
            AudioClip randomClip = swimSounds[randomIndex];

            // 播放音效
            audioSource.clip = randomClip;
            audioSource.Play();
        }
    }
    void SwimAction()
    {
    // 触发游泳动作代码
    

    // 播放游泳音效
    PlaySwimSound();
    }
}
