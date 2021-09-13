using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float turnSmoothTime = 0.1f;
    //Camera
    [SerializeField] private Vector3 cameraRelPosition;
    
    private Vector3 _moveDirection;

    [SerializeField]private Health health;
    [SerializeField] private HealthBar healthBar;
    
    private void Awake()
    {
        if (health != null && healthBar != null)
        {
            health.setHealthEvent += (sender, i) =>
            {
                healthBar.SetHealth(i);
            };

            health.setMaxHealthEvent += (sender, i) =>
            {
                healthBar.SetMaxHealth(i);
            };
        }
    }

    public void FixedUpdate()
    {
        MovePlayerAndCamera();
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

    private void MovePlayerAndCamera()
    {
        var cameraTransform = mainCamera.transform;
        var playerTransform = transform;

        //Get the forward direction respect to camera ( this points slightly down, therefore we set y = 0)
        //var cameraForward = cameraTransform.forward;
        var cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        
        //Relative rotation of camera respect to world
        var cameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, cameraForward);
        
        //Target look direction based on user input
        var lookToward = cameraRelativeRotation * _moveDirection;
        
        //If there is input
        if (_moveDirection.sqrMagnitude > 0)
        {
            //calculate the rotation angle to go from current direction to target direction
            var targetAngle = Vector3.SignedAngle(Vector3.forward, lookToward, Vector3.up);
            var targetRotation = Quaternion.Euler(0, targetAngle, 0);
            //smooth rotation so as not to have sharp movements
            var smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSmoothTime);
            //Debug.Log($"Target: {targetRotation}, smoothed: {smoothedRotation}");
            //apply rotation
            transform.rotation = smoothedRotation;
        }
        
        //Normalize speed
        var moveVelocity = playerTransform.forward * (moveSpeed * _moveDirection.sqrMagnitude);
        characterController.SimpleMove(moveVelocity);
        
        //Camera orientation
        var vectorCameraToPlayer = playerTransform.position - cameraRelPosition;
        cameraTransform.position = vectorCameraToPlayer;
        
        //Camera always looks at player
        cameraTransform.LookAt(playerTransform);

    }
    public void TakeDamageAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Damage taken");
            
            gameObject.GetComponent<Health>().TakeDamage(20);
        }
    }
}
