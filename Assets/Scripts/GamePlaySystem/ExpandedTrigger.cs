using System;
using Unity.Mathematics;
using UnityEngine;

public class ExpandedTrigger : MonoBehaviour
{
    public System.Action OnPlayerEnter;
    public System.Action OnPlayerExit;

    private void Start()
    {
        transform.localRotation = quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerExit?.Invoke();
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 红色表示扩展区域
        var collider = GetComponent<BoxCollider>();
        if (collider)
        {
            Gizmos.matrix = transform.localToWorldMatrix; // 确保Gizmo位置与对象对齐
            Gizmos.DrawWireCube(collider.center, collider.size);
        }
    }
}