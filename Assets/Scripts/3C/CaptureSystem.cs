using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class CaptureSystem : MonoBehaviour
{
    private HapticImpulsePlayer _hapticImpulsePlayer;

    private void Start()
    {
        _hapticImpulsePlayer = GetComponent<HapticImpulsePlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<trash>() && other.gameObject.CompareTag("Trash"))
        {
            other.GetComponent<trash>().TrashCaptured();
            _hapticImpulsePlayer.SendHapticImpulse(0.5f, 0.1f, 0);
        }
    }
}
