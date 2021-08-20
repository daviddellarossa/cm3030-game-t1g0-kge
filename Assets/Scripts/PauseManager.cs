using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    PauseAction action;
    public bool paused;

    public GameObject pauseMenu, optionsMenu;
    public GameObject pauseFirstButton, optionsFirstButton, optionsClosedButton;

    public void TogglePauseMenu()
    {
        if (!pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;

            // clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            // set a new selected object
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
        else {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            optionsMenu.SetActive(false);
        }
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }

    private void Awake() {
        action = new PauseAction();
    }

    private void OnEnable() {
        action.Enable();
    }

    private void OnDisable() {
        action.Disable();
    }

    private void Start() {
        action.Pause.PauseGame.performed += _ => DeterminePause();
    }

    private void DeterminePause() {
        if (paused)
            ResumeGame();
        else 
            PauseGame();
    }

    public void PauseGame() {
        Time.timeScale = 0;
        paused = true;
        TogglePauseMenu();
    }

    public void ResumeGame() {
        Time.timeScale = 0;
        paused = false;
        TogglePauseMenu();
    }
}

