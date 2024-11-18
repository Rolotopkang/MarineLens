using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject hintUIPanel; 

    public void OpenHintUI()
    {
        if (hintUIPanel != null)
        {
            hintUIPanel.SetActive(true); 
        }
    }

    public void CloseHintUI()
    {
        if (hintUIPanel != null)
        {
            hintUIPanel.SetActive(false); 
        }
    }
}
