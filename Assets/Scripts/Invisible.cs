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
        Debug.Log("invs");

        if (!isInvis) {
            foreach (var r in rend)
            {
                r.sharedMaterial = r.materials[1];
            }
      
            Debug.Log("inside the if");
            isInvis = !isInvis;
            Invoke(nameof(makeVisible), 5.0f);
        }
              
        
    }

    public void makeVisible()
    {
        Debug.Log("inside make vis");
        foreach (var r in rend)
        {
            r.sharedMaterial = r.materials[0];
        }
        isInvis = false;
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    
}
