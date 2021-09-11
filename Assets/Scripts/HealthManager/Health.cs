using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

// [Serializable]
// public class HealthEvent: UnityEvent<int, GameObject>{}
//
[Serializable]
public class CharacterEvent: UnityEvent<GameObject>{}




public class Health : MonoBehaviour
{
    public int maxHealth;

    public int currentHealth;
    
    public CharacterEvent hasDiedEvent = null;
    // public HealthEvent setHealthEvent = null;
    // public HealthEvent setMaxHealthEvent = null;
    
    public event EventHandler<int> setHealthEvent;
    public event EventHandler<int> setMaxHealthEvent;

    //public event EventHandler hasDiedEvent;
    
    public void ResetHealth()
    {
        currentHealth = maxHealth;

        // setMaxHealthEvent?.Invoke(maxHealth, gameObject);
        // setHealthEvent?.Invoke(maxHealth, gameObject);
        
        setMaxHealthEvent?.Invoke(gameObject, maxHealth);
        setHealthEvent?.Invoke(gameObject, maxHealth);

    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        // setHealthEvent?.Invoke(currentHealth, gameObject);
        setHealthEvent?.Invoke(gameObject, currentHealth);

        if (currentHealth <= 0)
        {
            hasDiedEvent?.Invoke(gameObject); 
            //hasDiedEvent?.Invoke(gameObject, new EventArgs()); 
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    public void Die()
    {
        if (currentHealth <= 0) return;
        TakeDamage(currentHealth);
    }
}
