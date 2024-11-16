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
    }

    private void Update()
    {
        playerManager.GetInstance().ChangeSpeed(_slider.value);
    }
}
