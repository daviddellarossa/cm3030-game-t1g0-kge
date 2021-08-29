using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("asdfpoiu");
        if (!collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
