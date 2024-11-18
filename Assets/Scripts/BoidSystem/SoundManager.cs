using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   
    // 单例实例
    public static SoundManager Instance;
    private AudioSource loopingEffectSource;
    // 背景音乐和音效的AudioSource
    public AudioSource musicSource;
    public AudioSource effectsSource;


    // 字典，用于存储所有的音效资源
    public AudioClip[] soundClips;

    void Awake()
    {
        // 实现单例模式
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 检查音频源是否被赋值
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();

        if (effectsSource == null)
            effectsSource = gameObject.AddComponent<AudioSource>();
        // 初始化循环播放的 AudioSource
        loopingEffectSource = gameObject.AddComponent<AudioSource>();
        loopingEffectSource.loop = true;  // 这里设置loop为true，确保循环播放
        loopingEffectSource.volume = 1.0f; // 默认音量为1
    }

    // 播放背景音乐
    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.loop = true; // 循环播放背景音乐
        musicSource.Play();
    }

    // 播放指定音效

    public void PlaySound(string clipName)
    {
        AudioClip clip = FindClipByName(clipName);
        if (clip != null)
        {
            effectsSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"音效 {clipName} 未找到！");
        }
    }
    // 播放循环音效
    public void PlayLoopingSound(string clipName)
    {
        AudioClip clip = FindClipByName(clipName);
        if (clip != null)
        {
            loopingEffectSource.clip = clip;
            loopingEffectSource.Play();
            Debug.Log($"开始循环播放音效: {clip.name}");
        }
        else
        {
            Debug.LogWarning($"音效 {clipName} 未找到！");
        }
    }

    // 停止循环音效
    public void StopLoopingSound()
    {
        if (loopingEffectSource.isPlaying)
        {
            loopingEffectSource.Stop();
        }
    }

    // 随机播放一组音效中的一个
    public void PlayRandomSound(params string[] clipNames)
    {
        string randomClipName = clipNames[Random.Range(0, clipNames.Length)];
        PlaySound(randomClipName);
    }

    // 停止背景音乐
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // 查找音效片段
    private AudioClip FindClipByName(string clipName)
    {
        foreach (AudioClip clip in soundClips)
        {
            if (clip.name == clipName)
                return clip;
        }
        return null;
    }
}
