using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_AddCleanPersentage : MonoBehaviour
{
    public void OnButtonClick()
    {
        AreaManager.GetInstance().CurrentArea().AddProgress();
    }
}
