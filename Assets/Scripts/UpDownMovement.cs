using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class UpDownMovement : MonoBehaviour
{
    XRInputValueReader<Vector2> m_RightHandMoveInput = new XRInputValueReader<Vector2>("Right Hand Move");

    private void Update()
    {
        // Debug.Log(m_RightHandMoveInput.ReadValue());
    }
}
