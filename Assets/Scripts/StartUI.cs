using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : Singleton<StartUI>
{
    public GameObject tpPoint;
    public AudioClip BGSound;

    private void Start()
    {
        playerManager.GetInstance().FreezePlayer(false);
        AudioManager.GetInstance().PlayBGSound(BGSound);
    }
    
    public void StartGame()
    {
        playerManager.GetInstance().transform.position = tpPoint.transform.position;
        playerManager.GetInstance().FreezePlayer(true);
    }
}
