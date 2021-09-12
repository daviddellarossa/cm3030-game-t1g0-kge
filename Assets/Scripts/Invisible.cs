using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine.InputSystem;
using UnityEngine;

public class Invisible : MonoBehaviour
{

    
    public Material[] material;
    public bool isInvis;
    Renderer[] rend;

    void Start()
    {
        rend = GetComponentsInChildren<Renderer>();
        foreach(var r in rend)
        {
            r.enabled = true;
            r.sharedMaterial = r.materials[0];
        }
        
        
        isInvis = false;
        
    }

    public void makeInvisible(/*InputAction.CallbackContext context*/)
    {
        Debug.Log(rend.Length);

        if (!isInvis) {
            foreach (var r in rend)
            {
                Debug.Log(r.materials[1]);
                r.sharedMaterial = material[1];
            }
            isInvis = !isInvis;
            Invoke(nameof(makeVisible), 5.0f);
        }
              
        
    }

    public void makeVisible()
    {
        foreach (var r in rend)
        {
            r.sharedMaterial = material[0];
        }
        isInvis = false;
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    
}
