using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndGame : MonoBehaviour
{
    private VideoPlayer VideoPlayer;


    private void Start()
    {
        VideoPlayer = GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        if (!VideoPlayer.isPlaying)
        {
            Application.Quit();
        }
    }
}
