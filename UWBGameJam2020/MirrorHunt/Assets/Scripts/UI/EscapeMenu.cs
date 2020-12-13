using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    public GameObject escapeMenu;

    private GameOverMenu m_gameOverMenu;
    private bool m_isMenuShown = false;

    // Show the escape menu
    public void SetIsShowMenu(bool isShown)
    {
        m_isMenuShown = isShown;
    }

    // Continue the game
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        escapeMenu.SetActive(false);
        Time.timeScale = 1f;
        m_isMenuShown = false;
    }

    // Go to game over menu
    public void Escape()
    {
        FindObjectOfType<AudioManager>().Pause("Alarm");
        m_gameOverMenu.AddScore(10);
        m_gameOverMenu.SignalInstantGameOver();
        Time.timeScale = 1f;
        escapeMenu.SetActive(false);
        m_isMenuShown = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_gameOverMenu = GameObject.FindObjectOfType<GameOverMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isMenuShown is true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            escapeMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
