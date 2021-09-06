using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batery_Pickup : MonoBehaviour
{
    [SerializeField] float restorationAngle = 80f;
    [SerializeField] float additiveIntensity = 2.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<Flashlight_System>().RestoreLightAngle(restorationAngle);
            other.GetComponentInChildren<Flashlight_System>().RestoreLightIntensity(additiveIntensity);
            Destroy(gameObject);
        }
    }
}
