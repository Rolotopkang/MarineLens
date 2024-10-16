using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwimController : MonoBehaviour
{
    public Transform LeftHand;
    public Transform RightHand;
    public Transform Player;
    public Transform CM;
    public float SpeedScale = 5f;
    public float waterFriction = 5f;
    public float maxSpeed = 4f;

    public bool UseCmForward = false;


    private Vector3 tmp_LeftPos;
    private Vector3 tmp_RightPos;
    private float speed;
    private float tmp_Distance;
    private CharacterController cc;
    
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        tmp_LeftPos = LeftHand.localPosition;
        tmp_RightPos = RightHand.localPosition;
        tmp_Distance = Mathf.Abs((LeftHand.localPosition - RightHand.localPosition).magnitude);
    }

    private void Update()
    {
        float currentDistance = Mathf.Abs((LeftHand.localPosition - RightHand.localPosition).magnitude);
        if (currentDistance > tmp_Distance)
        {
            float leftMovement = Mathf.Abs((LeftHand.localPosition - tmp_LeftPos).magnitude);
            float rightMovement = Mathf.Abs((RightHand.localPosition - tmp_RightPos).magnitude);
            speed += (leftMovement + rightMovement) * SpeedScale;
        }
        else
        {
            float leftMovement = Mathf.Abs((LeftHand.localPosition - tmp_LeftPos).magnitude);
            float rightMovement = Mathf.Abs((RightHand.localPosition - tmp_RightPos).magnitude);
            speed -= (leftMovement + rightMovement) * SpeedScale * 0.5f;
        }
        speed = Mathf.Clamp(speed, 0, maxSpeed);
        Vector3 currentV = UseCmForward
            ? CM.transform.forward * speed * Time.deltaTime
            : -(LeftHand.transform.position + RightHand.transform.position - new Vector3(0,0.136144f,0) - transform.position).normalized * speed * Time.deltaTime;
        speed *= waterFriction;
        cc.Move(currentV);
        tmp_LeftPos = LeftHand.localPosition;
        tmp_RightPos = RightHand.localPosition;
        tmp_Distance = currentDistance;
    }
}
