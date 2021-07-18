using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController_Mode2 : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController characterController;
    public Camera mainCamera;
    private Vector3 _moveDirection;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    

    public void FixedUpdate()
    {
        //Method1_From_PS();
        Method2_From_Brackeys();

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

    private void Method1_From_PS()
    {
        var cameraForward = GetComponent<Camera>().transform.forward;
        cameraForward.y = 0;
        
        var cameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, cameraForward);
        var lookToward = cameraRelativeRotation * _moveDirection;
        
        if (_moveDirection.sqrMagnitude > 0)
        {
            var lookRay = new Ray(transform.position, lookToward);
            transform.LookAt(lookRay.GetPoint(1));
        }
        
        var moveVelocity = transform.forward * moveSpeed * _moveDirection.sqrMagnitude;
        //moveVelocity = new Vector3(inputVector.x, 0f, inputVector.y);
        
        characterController.SimpleMove(moveVelocity);
    }

    private void Method2_From_Brackeys()
    {
        if (_moveDirection.magnitude == 0)
            return;
        
        float targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
        float angle =
            Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        characterController.SimpleMove(moveDir.normalized * moveSpeed);
    }
}
