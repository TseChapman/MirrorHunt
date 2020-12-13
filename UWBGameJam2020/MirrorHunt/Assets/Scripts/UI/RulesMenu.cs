using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesMenu : MonoBehaviour
{
    public GameObject firstUIPage;
    public GameObject secondUIPage;

    private LevelLoader m_levelLoader;

    public void SecondPage()
    {
        // Go to second page
        secondUIPage.SetActive(true);
        firstUIPage.SetActive(false);
    }

    public void PlayGame()
    {
        // Go to play
        m_levelLoader.NextLevel();
        SceneManager.LoadScene("Play");
    }

    private void Start()
    {
        m_levelLoader = GameObject.FindObjectOfType<LevelLoader>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
