using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;

    public int currentHealth;

    public HealthBar healthBar;

    public UnityEvent PlayerIsDead;
        

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            PlayerIsDead?.Invoke();
        }
        
    }
    
    public void TakeDamageAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Damage taken");
            TakeDamage(20);
        }
    }
}
