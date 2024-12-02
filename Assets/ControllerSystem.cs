using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSystem : Singleton<ControllerSystem>
{
    public ControllerHint Left;
    public ControllerHint Right;

    public void TriggerControllerHintLeft(int set)
    {
        Left.ShowHint(set , 10f);
    }
    
    public void TriggerControllerHintRight(int set)
    {
        Right.ShowHint(set,10f);
    }
    
    public void TriggerControllerHintLeft(int set , float time)
    {
        Left.ShowHint(set , time);
    }
    
    public void TriggerControllerHintRight(int set,float time)
    {
        Right.ShowHint(set,time);
    }

}
