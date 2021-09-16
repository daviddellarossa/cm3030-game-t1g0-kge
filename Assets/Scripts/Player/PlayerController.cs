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

    public InventoryObject inventory;

    [SerializeField] float restorationAngle = 80f;
    [SerializeField] float additiveIntensity = 2.5f;

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

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Debug.Log("Pressed Key1");
            CheckItemExists(1);
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            Debug.Log("Pressed Key2");
            CheckItemExists(2);
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            Debug.Log("Pressed Key3");
            CheckItemExists(3);
        }
    }

    private void CheckItemExists(int keyNumber)
    {
        InventorySlot slot = inventory.Container.Items[keyNumber - 1];
        //get the total amount of items in the inventory
        int itemCount = inventory.Container.Items.Count;
        if (slot.amount > 0 && itemCount > 0)
        {
            //use item
            Debug.Log("Item exists");
            UseItem(slot);
        }
    }

    //use the item in the inventory slot
    private void UseItem(InventorySlot slot)
    {
        //get the id of the item located in that slot
        int itemID = slot.item.Id;
        if (itemID == 0)
        {
            Debug.Log("This is a BandAid");
            inventory.updateUsedItem(slot);
            RestoreHealth(10);
        }
        if (itemID == 1)
        {
            Debug.Log("This is a Health Potion");
            inventory.updateUsedItem(slot);
            RestoreHealth(40);
        }
        if (itemID == 2)
        {
            Debug.Log("This is a key.");
            inventory.updateUsedItem(slot);
        }
        if (itemID == 3)
        {
            Debug.Log("This is a battery.");
            inventory.updateUsedItem(slot);
            this.GetComponentInChildren<Flashlight_System>().RestoreLightAngle(restorationAngle);
            this.GetComponentInChildren<Flashlight_System>().RestoreLightIntensity(additiveIntensity);
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

    //restore health
    public void RestoreHealth(int amountToRestore)
    {
        health.RestoreHealth(amountToRestore);
    }
}
