using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            if (collision.collider.CompareTag("Player"))
            {
                GameObject.Find("Player").GetComponent<Health>().TakeDamage(20);
            }
        }
    }
}
