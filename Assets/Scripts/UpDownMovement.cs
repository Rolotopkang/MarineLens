using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;

public class UpDownMovement : LocomotionProvider
{
    [SerializeField]
    XRInputValueReader<Vector2> m_RightHandMoveInput;  // Removed initialization here, assign in Inspector
    
    public float VerticalSpeed = 5f;
    public bool canMove;
    private InputAction moveAction;
    CharacterController m_CharacterController;
    public XROriginMovement transformation { get; set; } = new XROriginMovement();

    bool m_AttemptedGetCharacterController;

    Vector3 m_VerticalVelocity;

    private void OnEnable()
    {
        moveAction = m_RightHandMoveInput.inputActionReference;
        moveAction.Enable();
    }

    private void OnDisable()
    {
        // Disable action when the object is disabled
        if (moveAction != null)
        {
            moveAction.Disable();
        }
    }

    private void Update()
    {
        if (moveAction != null && moveAction.enabled)
        {
            MoveRig();
        }   
    }
    
    
    protected virtual void MoveRig()
    {
        var xrOrigin = mediator.xrOrigin?.Origin;
        if (xrOrigin == null)
            return;
        if (moveAction.ReadValue<Vector2>().y>0 && !canMove)
        {
            return;
        }
        FindCharacterController();

        Vector3 motion = new Vector3();
        if (m_CharacterController != null && m_CharacterController.enabled)
        {
            m_VerticalVelocity =  new Vector3(0, moveAction.ReadValue<Vector2>().y,0);
            motion += m_VerticalVelocity * Time.deltaTime * VerticalSpeed;
        }

        TryStartLocomotionImmediately();

        if (locomotionState != LocomotionState.Moving)
            return;
        
        transformation.motion = motion;
        TryQueueTransformation(transformation);
    }
    
    void FindCharacterController()
    {
        var xrOrigin = mediator.xrOrigin?.Origin;
        if (xrOrigin == null)
            return;

        // Save a reference to the optional CharacterController on the rig GameObject
        // that will be used to move instead of modifying the Transform directly.
        if (m_CharacterController == null && !m_AttemptedGetCharacterController)
        {
            // Try on the Origin GameObject first, and then fallback to the XR Origin GameObject (if different)
            if (!xrOrigin.TryGetComponent(out m_CharacterController) && xrOrigin != mediator.xrOrigin.gameObject)
                mediator.xrOrigin.TryGetComponent(out m_CharacterController);

            m_AttemptedGetCharacterController = true;
        }
    }
}