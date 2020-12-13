using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject controlsMenuUI;

    // Update is called once per frame
    private void Update()
    {
        // TODO: Not pause menu in gameover UI
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
        }
    }

    // Continue to play and inactive the pause menu
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Pause the game and show the pause menu
    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Turn on the control menu
    public void Controls()
    {
        controlsMenuUI.SetActive(true);
    }

    // Back to the pause menu
    public void Back()
    {
        controlsMenuUI.SetActive(false);
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
