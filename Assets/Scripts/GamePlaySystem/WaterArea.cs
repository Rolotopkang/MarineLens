
using StylizedWater2.UnderwaterRendering;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
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
    public bool hasBeenCleaned;

    public UnityEvent EndCleanEvent;
    
    public UnityEvent OnEnterWaterArea;
    public UnityEvent OnExitWaterArea;

    private BoxCollider[] myCollider;
    private Camera currentCamera;
    private int CleanedCount = 0;
    private bool isChanging = false;
    private UnderwaterSettings _underwaterSettings;
    private Volume _volume;
    private GameObject expandedTrigger;
    public int TriggerCurrentAreaNum;
    public int TriggerPlayerInNum;
    
    private void Start()
    {
        myCollider = GetComponents<BoxCollider>();
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
        InitializeExpandedTrigger();
    }
    
    private void InitializeExpandedTrigger()
    {
        var originalColliders = GetComponents<BoxCollider>(); // 获取所有 BoxCollider

        foreach (var originalCollider in originalColliders)
        {
            // 为每个 BoxCollider 创建一个扩展触发器
            var expandedTrigger = new GameObject($"ExpandedTrigger_{originalCollider.name}");
            expandedTrigger.transform.SetParent(transform);
            expandedTrigger.transform.localPosition = Vector3.zero;

            expandedTrigger.layer = gameObject.layer;

            var collider = expandedTrigger.AddComponent<BoxCollider>();
            collider.isTrigger = true;

            // 设置扩展的中心和大小
            collider.center = originalCollider.center;
            collider.size = originalCollider.size + Vector3.one * blendDistance * 2;
            
            // 添加扩展触发脚本
            var triggerScript = expandedTrigger.AddComponent<ExpandedTrigger>();
            triggerScript.OnPlayerEnter = () => { TriggerPlayerInNum++; OnEnterWaterArea?.Invoke(); };
            triggerScript.OnPlayerExit = () => { TriggerPlayerInNum--; OnExitWaterArea?.Invoke(); };
        }
    }



    
    private void Update()
    {
        UpdateAreaCheck();
        
        if (isChanging)
        {
            CleanPersentage = Mathf.Lerp(CleanPersentage, (float)CleanedCount/Trashes.Length, ChangeSpeed * Time.deltaTime);
            
            if (Mathf.Abs(CleanPersentage - (float)CleanedCount/Trashes.Length) < 0.001f)
            {
                CleanPersentage = (float)CleanedCount/Trashes.Length;
                isChanging = false;
            }
        }

        if (CleanPersentage >= 1 && !hasBeenCleaned)
        {
            EndCleanEvent.Invoke();
            hasBeenCleaned = true;
        }

        BaseColor = Color.Lerp(InitBaseColor, TargetBaseColor, colorChangeCurve.Evaluate(CleanPersentage) );
        ShallowColor = Color.Lerp(InitShallowColor, TargetShallowColor, colorChangeCurve.Evaluate(CleanPersentage));

        UpdateVolume();
    }

    private void UpdateAreaCheck()
    {
        isCurrentArea = TriggerCurrentAreaNum >= 1;
        isPlayerIn = TriggerPlayerInNum >= 1;
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCurrentArea = true;
            TriggerCurrentAreaNum++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerCurrentAreaNum--;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; // 蓝色表示原始区域
        var colliders = GetComponents<BoxCollider>();
        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                Gizmos.matrix = collider.transform.localToWorldMatrix; // 确保Gizmo位置与对象对齐
                Gizmos.DrawWireCube(collider.center, collider.size);
            }
        }
    }
    
    public float GetCleanCount => CleanedCount;
}
