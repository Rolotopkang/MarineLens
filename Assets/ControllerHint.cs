using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHint : MonoBehaviour
{
    private int tmp_Set;

    private void Start()
    {
        SetController(false);
    }

    public void ShowHint(int set , float time)
    {
        resetUI();
        SetController(true);
        transform.GetChild(1).GetChild(set).gameObject.SetActive(true);
        tmp_Set = set;
        Invoke(nameof(DelUI),time);
    }

    public void DelUI()
    {
        transform.GetChild(1).GetChild(tmp_Set).gameObject.SetActive(false);
        SetController(false);
    }

    public void SetController(bool set)
    {
        transform.GetChild(0).gameObject.SetActive(set);
    }
    
    public void resetUI()
    {
        foreach (Transform child in transform.GetChild(1))
        {
            child.gameObject.SetActive(false);
        }
    }
}
