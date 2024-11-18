using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class trash_Logo : MonoBehaviour
{
    public float MaxSize = 1.2f;
    public float MinSize = 0.8f;
    public UnityEvent CapturedEvent;
    public AudioClip AudioClip;


    private void Start()
    {
        transform.localScale *= Random.Range(MinSize, MaxSize);
    }

    public void TrashCaptured()
    {
        AudioManager.GetInstance().PlaySound(AudioClip,transform.position,false,50);
        StartUI.GetInstance().StartGame();
        CapturedEvent.Invoke();
        gameObject.SetActive(false);
    }
}
