using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public void CharacterHasDied(GameObject gameObject)
    {
        Debug.Log($"GameObject {gameObject.name} dies");
        Destroy(gameObject);
    }

    public void HintTriggereEnter_EventHandler(HintMessage hint)
    {
        Debug.Log(hint.text);
        var hintDisplay = GameObject.FindGameObjectWithTag("HintDisplay");
        var textMeshPro = hintDisplay.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = hint.text;
        Invoke(nameof(ClearMessage), hint.duration);

    }
    

    
    public void HintTriggereExit_EventHandler()
    {
        //Debug.Log("Clear message");
    }

    public void ClearMessage()
    {
        var hintDisplay = GameObject.FindGameObjectWithTag("HintDisplay");
        var textMeshPro = hintDisplay.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = String.Empty;
    }
}
