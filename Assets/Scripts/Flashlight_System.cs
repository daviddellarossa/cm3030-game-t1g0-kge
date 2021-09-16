using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight_System : MonoBehaviour
{
    [SerializeField] float minimumAngle = 50f;
    [SerializeField] float angleDecay = 5f;
    [SerializeField] float lightDecay = 2.5f;

    [SerializeField] float maximumIntensity = 15f;

    Light flashlight;

    void Start()
    {
        flashlight = GetComponent<Light>();
    }


    void Update()
    {
        DescreaseLightAngle();
        DecreaseLightIntensity();
    }

    public void RestoreLightAngle(float angle)
    {
        flashlight.spotAngle = angle;
    }

    public void RestoreLightIntensity(float intensity)
    {
        if (flashlight.intensity + intensity > maximumIntensity)
        {
            flashlight.intensity = maximumIntensity;
        }
        else
        {
            flashlight.intensity += intensity;
        }

    }

    private void DescreaseLightAngle()
    {
        if (flashlight.spotAngle > minimumAngle)
        {
            flashlight.spotAngle -= angleDecay * Time.deltaTime;
        }
        return;
    }

    private void DecreaseLightIntensity()
    {
        flashlight.intensity -= lightDecay * Time.deltaTime;

    }
}
