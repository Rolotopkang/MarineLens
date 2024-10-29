using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    underwater,
    wave
}

[RequireComponent(typeof(AudioSource))]
public class SoundManagement : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManagement instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
}
