using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class AreaManager : Singleton<AreaManager>
{
    public Renderer waterRenderer;
    public bool DebugMode = false;
    [ColorUsage(true,true)]
    public Color InitDeepColor;
    [ColorUsage(true,true)]
    public Color InitShallowColor;
    public List<WaterArea> activeRegions = new List<WaterArea>();
    
    
    private Camera currentCamera;
    
    void Start()
    {
        if (waterRenderer == null) Debug.LogWarning("我水呢？");
        if (waterRenderer != null)
        {
            InitDeepColor = waterRenderer.sharedMaterial.GetColor("_BaseColor");
            InitShallowColor= waterRenderer.sharedMaterial.GetColor("_ShallowColor");
        }
        currentCamera = Camera.main;
    }
    
    private void OnDisable()
    {
        if (DebugMode)
        {
            waterRenderer.sharedMaterial.SetColor("_BaseColor", InitDeepColor);
            waterRenderer.sharedMaterial.SetColor("_ShallowColor", InitShallowColor);
        }
    }

    private void Update()
    {
        if (currentCamera == null) return;
        Color blendedDeepColor = InitDeepColor;
        Color blendedShallowColor = InitShallowColor;
        
        float totalWeight = 0.0f;

        foreach (WaterArea region in activeRegions)
        {
            BoxCollider boxCollider = region.GetComponent<BoxCollider>();
            
            if (boxCollider == null) continue;
            
            if (!region.isPlayerIn) { continue; }


            float distance = Vector3.Distance(currentCamera.transform.position, boxCollider.ClosestPoint(currentCamera.transform.position));
            float blendFactor = Mathf.Clamp01(1 - (distance / region.blendDistance));

            blendedDeepColor = Color.Lerp(blendedDeepColor, region.BaseColor, blendFactor);
            blendedShallowColor = Color.Lerp(blendedShallowColor, region.ShallowColor, blendFactor);
            
            totalWeight += blendFactor;
        }
        
        waterRenderer.sharedMaterial.SetColor("_BaseColor", blendedDeepColor);
            
        Color emissionShallowColor = blendedShallowColor * totalWeight;
        waterRenderer.sharedMaterial.SetColor("_ShallowColor", blendedShallowColor);
    }
}
