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
        AutoHandPlayer.useMovement = set;
    }
    
    
}
