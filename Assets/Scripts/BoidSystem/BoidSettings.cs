using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoidSettings : ScriptableObject {
    // Settings
    [Header("速度")]
    public float minSpeed = 2;
    public float maxSpeed = 5;
    public float panicMaxSpeed = 20;
    
    [Header("恐慌设置")]
    public float panicTime = 1f;
    public float perceptionRadius = 2.5f;
    public float avoidanceRadius = 1;
    public float maxSteerForce = 3;
    
    [Header("恐慌避障权重")]
    public float avoidMovingCollisionWeight = 500;
    public float movingCollisionAvoidDst = 1;

    [Header("群聚相关")]
    public float alignWeight = 1;
    public float cohesionWeight = 1;
    public float seperateWeight = 1;

    [Header("追踪相关")]
    public float targetWeight = 1;
    public float targetWeightInPanic = 0.2f;

    [Header("避障相关")]
    public LayerMask obstacleMask;
    public LayerMask movingObstacleMask;
    public float boundsRadius = .27f;
    public float avoidCollisionWeight = 10;
    public float collisionAvoidDst = 5;


}