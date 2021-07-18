using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController_Mode1 : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController characterController;
    public Camera mainCamera;
    private Vector3 _moveDirection;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public void FixedUpdate()
    {
        Method1();

    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    public void Move(InputAction.CallbackContext context)
    {
        var inputVector = context.ReadValue<Vector2>();
        Debug.Log($"Move: {inputVector}");
        
        _moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
    }

    private void Method1()
    {
        var cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;
        
        var cameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, cameraForward);
        var lookToward = cameraRelativeRotation * _moveDirection;
        
        if (_moveDirection.sqrMagnitude > 0)
        {
            var lookRay = new Ray(transform.position, lookToward);
            transform.LookAt(lookRay.GetPoint(1));
        }
        
        var moveVelocity = transform.forward * (moveSpeed * _moveDirection.sqrMagnitude);

        characterController.SimpleMove(moveVelocity);
        
    }
    
}
