
using UnityEditor;
using UnityEngine;



public class WaterArea : MonoBehaviour
{
    public string AreaName;
    
    [ColorUsage(true, true)]
    public Color InitBaseColor = Color.blue;
    
    [ColorUsage(true, true)]
    public Color InitShallowColor = Color.blue;
    
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
    private void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        AreaManager.GetInstance().activeRegions.Add(this);
        currentCamera = Camera.main;
        BaseColor = InitBaseColor;
        ShallowColor = InitShallowColor;
    }
    
    private void Update()
    {
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
    
}
