using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UI_Tutorial : MonoBehaviour
{
    public GameObject Slot;
    public List<UI_Sentence> list = new List<UI_Sentence>(); 
    public int CurrentPage = 0;
    public UnityEvent EndEvent;


    private void Start()
    {
        foreach (UI_Sentence sentence in list)
        {
            sentence.gameObject.SetActive(false);
        }
        list[CurrentPage].gameObject.SetActive(true);
        list[CurrentPage].Trigger();
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
        if (EndEvent!=null)
        {
            EndEvent.Invoke();
        }
        Destroy(gameObject);
    }
    
    
}
