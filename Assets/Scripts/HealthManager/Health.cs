using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[Serializable]
public class IntEvent : UnityEvent<int> { }
[Serializable]
public class HealthEvent: UnityEvent<int, GameObject>{}
[Serializable]
public class CharacterEvent: UnityEvent<GameObject>{}
public class Health : MonoBehaviour
{
    public int maxHealth;

    public int currentHealth;

    public UnityEvent isDeadEvent;
    public IntEvent setHealthEvent;
    public IntEvent setMaxHealthEvent;

    public CharacterEvent hasDiedEvent = null;
    public HealthEvent setHealthEvent1 = null;
    public HealthEvent setMaxHealthEvent1 = null;
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        setMaxHealthEvent?.Invoke(maxHealth);
        setHealthEvent?.Invoke(maxHealth);
        
        setMaxHealthEvent1?.Invoke(maxHealth, gameObject);
        setHealthEvent1?.Invoke(maxHealth, gameObject);

    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        setHealthEvent?.Invoke(currentHealth);
        setHealthEvent1?.Invoke(currentHealth, gameObject);

        if (currentHealth <= 0)
        {
            hasDiedEvent?.Invoke(gameObject); 
        }
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }
}
