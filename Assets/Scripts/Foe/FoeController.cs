using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FoeController : MonoBehaviour
{
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
    
    public void TakeDamageAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Damage taken");
            
            gameObject.GetComponent<Health>().TakeDamage(20);
        }
    }
}
