
using StylizedWater2.UnderwaterRendering;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


public class WaterArea : MonoBehaviour
{
    public string AreaName;

    public trash[] Trashes;

    public float CleanPersentage = 0;

    public float ChangeSpeed = 5f;
    

    [ColorUsage(true, true)]
    public Color InitBaseColor = Color.blue;
    
    [ColorUsage(true, true)]
    public Color InitShallowColor = Color.blue;
    
    [ColorUsage(true, true)]
    public Color TargetBaseColor = Color.blue;
    
    [ColorUsage(true, true)]
    public Color TargetShallowColor = Color.blue;
    
    [HideInInspector]
    [ColorUsage(true, true)]
    public Color BaseColor = Color.blue;
    
    [HideInInspector]
    [ColorUsage(true,true)]
    public Color ShallowColor = Color.blue;
    public float blendDistance = 15.0f;
    public AnimationCurve colorChangeCurve;

    [Header("区域后处理设置(曲线图为0~1区间)")] 
    public float InitStartDistance = 0;
    public float TargetStartDistance = 0;
    public AnimationCurve startDistanceCurve;
    
    public float InitFogDensity = 0;
    public float TargetFogDensity = 0;
    public AnimationCurve FogDensityCurve;
    
    public float InitHeightFogDepth= 0;
    public float TargetHeightFogDepth = 0;
    public AnimationCurve HeightFogDepthCurve;
    
    public float InitHeightFogDensity = 0;
    public float TargetHeightFogDensity = 0;
    public AnimationCurve HeightFogDensityCurve;
    
   
    public bool isPlayerIn = false;
    public bool isCurrentArea;

    private BoxCollider myCollider;
    private Camera currentCamera;
    private int CleanedCount = 0;
    private bool isChanging = false;
    private UnderwaterSettings _underwaterSettings;
    private Volume _volume;
    
    private void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        AreaManager.GetInstance().activeRegions.Add(this);
        currentCamera = Camera.main;
        BaseColor = InitBaseColor;
        ShallowColor = InitShallowColor;
        Trashes = GetComponentsInChildren<trash>();
        _volume = GetComponent<Volume>();
        
        
        if (_volume.profile.TryGet(out _underwaterSettings))
        {
            _underwaterSettings.startDistance.value = InitStartDistance;
            _underwaterSettings.fogDensity.value = InitFogDensity;
            _underwaterSettings.heightFogDepth.value = InitHeightFogDepth;
            _underwaterSettings.heightFogDensity.value = InitHeightFogDensity;
        }
        
    }
    
    private void Update()
    {
        if (isChanging)
        {
            CleanPersentage = Mathf.Lerp(CleanPersentage, (float)CleanedCount/Trashes.Length, ChangeSpeed * Time.deltaTime);
            
            if (Mathf.Abs(CleanPersentage - (float)CleanedCount/Trashes.Length) < 0.001f)
            {
                CleanPersentage = (float)CleanedCount/Trashes.Length;
                isChanging = false;
            }
        }

        BaseColor = Color.Lerp(InitBaseColor, TargetBaseColor, colorChangeCurve.Evaluate(CleanPersentage) );
        ShallowColor = Color.Lerp(InitShallowColor, TargetShallowColor, colorChangeCurve.Evaluate(CleanPersentage));

        UpdateVolume();
        
        if (currentCamera)
        {
            Bounds extendedBounds = myCollider.bounds;
            extendedBounds.Expand(blendDistance * 2);
            
            if (extendedBounds.Contains(currentCamera.transform.position))
            {
                isPlayerIn = true;
            }
            else
            {
                isPlayerIn = false;
            }

            isCurrentArea = myCollider.bounds.Contains(currentCamera.transform.position);
        }
    }


    private void UpdateVolume()
    {
        _underwaterSettings.startDistance.value = Mathf.Lerp(InitStartDistance, TargetStartDistance,
            startDistanceCurve.Evaluate(CleanPersentage));
        _underwaterSettings.fogDensity.value =
            Mathf.Lerp(InitFogDensity, TargetFogDensity, FogDensityCurve.Evaluate(CleanPersentage));
        _underwaterSettings.heightFogDepth.value = Mathf.Lerp(InitHeightFogDepth, TargetHeightFogDepth,
            HeightFogDepthCurve.Evaluate(CleanPersentage));
        _underwaterSettings.heightFogDensity.value = Mathf.Lerp(InitHeightFogDensity, TargetFogDensity,
            FogDensityCurve.Evaluate(CleanPersentage));
    }
    
    
    public void AddProgress()
    {
        CleanedCount++;
        if (CleanedCount > Trashes.Length)
            CleanedCount = Trashes.Length;
        
        if (!isChanging)
        {
            isChanging = true;
        }
    }

    public float GetCleanCount => CleanedCount;
}
