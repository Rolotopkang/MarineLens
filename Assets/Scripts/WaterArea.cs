
using UnityEditor;
using UnityEngine;



public class WaterArea : MonoBehaviour
{
    [ColorUsage(true, true)]  // 启用 HDR 支持
    public Color BaseColor = Color.blue;  // 目标颜色
    [ColorUsage(true,true)]
    public Color ShallowColor = Color.blue;
    public float blendDistance = 15.0f;  // 过渡距离（米）

    private BoxCollider myCollider;
    public bool isPlayerIn = false;

    private Camera currentCamera;
    private void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        AreaManager.GetInstance().activeRegions.Add(this);
        currentCamera = Camera.main;
    }
    
    private void Update()
    {
        if (currentCamera)
        {
            // 增加过渡距离，创建扩展后的 Bounds
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
        }
    }
    
}
