using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class HintMessage
{
    [Multiline]
    public string text;
    [Range(1, 20)] public int duration;
}

[Serializable]
public class HintTriggeredEnterEvent : UnityEvent<HintMessage>{}

public class HintTrigger : MonoBehaviour
{
    public HintMessage hint;
    
    public bool fireOnlyOnce = false;
    public bool fired = false;
    
    public HintTriggeredEnterEvent HintTriggeredEnter;
    
    public UnityEvent HintTriggeredExit;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!(fireOnlyOnce & fired))
        {
            HintTriggeredEnter?.Invoke(hint);
            fired = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!(fireOnlyOnce & fired))
        {
            HintTriggeredExit?.Invoke();
        }
    }

}
