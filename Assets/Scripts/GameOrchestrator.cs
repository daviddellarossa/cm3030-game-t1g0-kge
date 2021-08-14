using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameOrchestrator : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //RespawnPlayer();

        // var playerInput = player.GetComponent<PlayerInput>();
        // playerInput.camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDies()
    {
        Debug.Log("Player dies");
        Destroy(player);
        
        Invoke(nameof(RespawnPlayer), 2);
    }

    private void RespawnPlayer()
    {
        Debug.Log("Respawning Player");
        //player = GameObject.Instantiate(player);
    }
}
