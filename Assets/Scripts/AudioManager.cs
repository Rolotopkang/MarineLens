using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // 单例模式

    public GameObject audioSourcePrefab; // 引用 AudioSourcePrefab
    public AudioSource backgroundMusicSource; // 背景音乐的 AudioSource
    public AudioClip backgroundMusicClip; // 背景音乐音频剪辑

    void Awake()
    {
        // 确保只有一个 AudioManager 实例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持跨场景存在
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 启动时播放背景音乐
        if (backgroundMusicSource != null && backgroundMusicClip != null)
        {
            PlayBackgroundMusic(backgroundMusicClip);
        }
    }

    /// <summary>
    /// 播放背景音乐（循环）
    /// </summary>
    /// <param name="clip">背景音乐剪辑</param>
    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.clip = clip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogError("背景音乐的 AudioSource 未设置！");
        }
    }

    /// <summary>
    /// 播放音效，播放完后销毁 GameObject
    /// </summary>
    /// <param name="clip">音效剪辑</param>
    /// <param name="position">播放位置</param>
    /// <param name="loop">是否循环播放</param>
    public void PlaySound(AudioClip clip, Vector3 position, bool loop = false)
    {
        if (clip == null || audioSourcePrefab == null)
        {
            Debug.LogError("缺少音效剪辑或 AudioSourcePrefab！");
            return;
        }

        // 创建音频对象
        GameObject audioObject = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        AudioSource audioSource = audioObject.GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();

        // 如果不循环，播放完后销毁对象
        if (!loop)
        {
            Destroy(audioObject, clip.length);
        }
    }
}
