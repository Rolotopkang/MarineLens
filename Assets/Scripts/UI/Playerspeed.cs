using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playerspeed : MonoBehaviour
{
   private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(ChangeSpeed);
    }
    
    private void ChangeSpeed(float value)
    {
        playerManager.GetInstance().ChangeSpeed(value);
    }
}
