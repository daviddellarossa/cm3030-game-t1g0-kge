using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float turnSmoothTime = 0.1f;
    
    private Vector3 movement;

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
        MovePlayer();
        if (transform.position.y < -10)
        {
            health.Die();
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    public void Move(InputAction.CallbackContext context)
    {
        var inputVector = context.ReadValue<Vector2>();
        Debug.Log($"Move: {inputVector}");
        
        movement = new Vector3(inputVector.x, 0f, inputVector.y);
        
    }

    private void MovePlayer()
    {
        var cameraTransform = mainCamera.transform;
        var playerTransform = transform;

        var move = new Vector3(0, 0, movement.z);
        var strafe = new Vector3(movement.x, 0, 0);

        //Get the forward direction respect to camera ( this points slightly down, therefore we set y = 0)
        var cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        
        //Target look direction based on user input
        var lookToward = cameraForward * Math.Sign(movement.z);

        //If there is input
        if (move.sqrMagnitude > 0)
        {
            //calculate the rotation angle to go from current direction to target direction
            var targetAngle = Vector3.SignedAngle(Vector3.forward, lookToward, Vector3.up);
            var targetRotation = Quaternion.Euler(0, targetAngle, 0);
            //smooth rotation so as not to have sharp movements
            var smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSmoothTime);
            //apply rotation
            transform.rotation = smoothedRotation;
        }
        var moveVelocity = playerTransform.forward * (moveSpeed * move.magnitude);

        //Apply strafe to moveVelocity
        if (strafe.sqrMagnitude > 0)
        {
            //strafe is always relative to camera
            var strafeVelocity = cameraTransform.right * (moveSpeed * strafe.magnitude * Math.Sign(strafe.x));
            moveVelocity += strafeVelocity;
        }
        
        //Normalize speed
        characterController.SimpleMove(moveVelocity);
        
    }
    public void TakeDamageAction(InputAction.CallbackContext context)
    {       
        if (context.performed)
        {
            Debug.Log("Damage taken");
            
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }
}
