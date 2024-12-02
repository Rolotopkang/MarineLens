using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class playerManager : Singleton<playerManager>
{
    public AutoHandPlayer AutoHandPlayer;
    
    
    public void ChangeSpeed(float set)
    {
        AutoHandPlayer.maxMoveSpeed = set;
    }

    public void FreezePlayer(bool set)
    {
        if (!set)
        {
            AutoHandPlayer.maxMoveSpeed = 0;
        }
        else
        {
            AutoHandPlayer.maxMoveSpeed = 7.5f;
        }
    }
    
    
}
