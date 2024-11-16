using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaCleanPercentage : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;
    public Renderer sphereRenderer;
    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (AreaManager.GetInstance().CurrentArea() != null)
        {
            _textMeshProUGUI.text = AreaManager.GetInstance().CurrentArea().CleanPersentage.ToString("P0").Replace("%", "");
            float cleanPercentage = AreaManager.GetInstance().CurrentArea().CleanPersentage;
            float shaderProgress = Mathf.Lerp(-1f, 1f, cleanPercentage);
            sphereRenderer.material.SetFloat("_Float", shaderProgress);

        }
        else
        {
            _textMeshProUGUI.text = "Clean Process";
        }
    }
}
