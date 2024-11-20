using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_Tutorial : MonoBehaviour
{
    public GameObject Slot;
    public List<UI_Sentence> list = new List<UI_Sentence>(); 
    public int CurrentPage = 0;


    private void Start()
    {
        foreach (UI_Sentence sentence in list)
        {
            sentence.gameObject.SetActive(false);
        }
        list[CurrentPage].gameObject.SetActive(true);
        list[CurrentPage].Trigger();x   
        CurrentPage++;
    }

    private void OnEnable()
    {

    }

    public void NextPage()
    {
        if (CurrentPage  == list.Count)
        {
            Invoke(nameof(EndUi),1f);
            return;
        }
        foreach (UI_Sentence sentence in list)
        {
            sentence.gameObject.SetActive(false);
        }

        list[CurrentPage].gameObject.SetActive(true);
        list[CurrentPage].Trigger();
        CurrentPage++;
    }

    public void EndUi()
    {
        Destroy(gameObject);
    }
    
    
}
