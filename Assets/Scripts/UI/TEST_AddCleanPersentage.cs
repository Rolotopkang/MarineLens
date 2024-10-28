using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_AddCleanPersentage : MonoBehaviour
{
    public WaterArea WaterArea;


    public void OnButtonClick()
    {
        WaterArea.AddProgress();
    }
}
