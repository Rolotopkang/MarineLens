using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class CaptureSystem : MonoBehaviour
{
    public Transform Center;
    private HapticImpulsePlayer _hapticImpulsePlayer;

    private void Start()
    {
        _hapticImpulsePlayer = GetComponent<HapticImpulsePlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<trash>() && other.gameObject.CompareTag("Trash"))
        {
            
            Vector3 tmp_directionToOther = (other.transform.position - Center.position).normalized;
            float tmp_dot = Vector3.Dot(-transform.right, tmp_directionToOther);
            if (tmp_dot < 0)
            {
                other.GetComponent<trash>().TrashCaptured();
                _hapticImpulsePlayer.SendHapticImpulse(0.5f, 0.1f, 0);
            }
        }
    }
}
