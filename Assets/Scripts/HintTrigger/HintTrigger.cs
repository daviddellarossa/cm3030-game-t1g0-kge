using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class HintTriggeredEnterEvent : UnityEvent<string>{}

[Serializable]
public class HintTriggeredEnterEvent2 : UnityEvent<TextMeshPro>{}

public class HintTrigger : MonoBehaviour
{
    public string message;
    public bool fireOnlyOnce = false;
    public bool fired = false;
    
    public HintTriggeredEnterEvent HintTriggeredEnter;
    public HintTriggeredEnterEvent2 HintTriggeredEnter2;
    
    public UnityEvent HintTriggeredExit;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!(fireOnlyOnce & fired))
        {
            HintTriggeredEnter?.Invoke(message);
            var tmp = GetComponent<TextMeshPro>();
            HintTriggeredEnter2?.Invoke(tmp);
            fired = true;
        }
    }

    private void BuildMessage()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!(fireOnlyOnce & fired))
        {
            HintTriggeredExit?.Invoke();
        }
    }

}
