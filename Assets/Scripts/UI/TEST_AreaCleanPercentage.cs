using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TEST_AreaCleanPercentage : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;
    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (AreaManager.GetInstance().CurrentArea() != null)
        {
            _textMeshProUGUI.text = AreaManager.GetInstance().CurrentArea().CleanPersentage.ToString("P");
        }
        else
        {
            _textMeshProUGUI.text = "No Current Area";
        }
    }
}
