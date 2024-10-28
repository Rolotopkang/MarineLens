
using UnityEditor;
using UnityEngine;



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

    private BoxCollider myCollider;
    public bool isPlayerIn = false;
    public bool isCurrentArea;

    private Camera currentCamera;
    private int CleanedCount = 0;
    private bool isChanging = false;
    
    private void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        AreaManager.GetInstance().activeRegions.Add(this);
        currentCamera = Camera.main;
        BaseColor = InitBaseColor;
        ShallowColor = InitShallowColor;
        Trashes = GetComponentsInChildren<trash>();
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

        BaseColor = Color.Lerp(InitBaseColor, TargetBaseColor, CleanPersentage);
        ShallowColor = Color.Lerp(InitShallowColor, TargetShallowColor, CleanPersentage);

        
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
    
}
