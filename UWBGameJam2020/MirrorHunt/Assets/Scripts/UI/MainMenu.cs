using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject controlMenuUI;
    public GameObject creditDesignMenuUI;
    public GameObject creditAssetsMenuUI;
    public GameObject creditAudioMenuUI;

    private LevelLoader m_levelLoader;

    public void Play()
    {
        // Go to tutorial scene to go over rules
        m_levelLoader.NextLevel();
        SceneManager.LoadScene("Rules");
    }

    public void Control()
    {
        // Show control page
        controlMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void BackMainMenu()
    {
        // Comeback to main menu from control
        mainMenuUI.SetActive(true);
        controlMenuUI.SetActive(false);
        
    }

    public void Credit()
    {
        // Go to Credit Design page
        creditDesignMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }


    public void CreditBackMenu()
    {
        // Go back to Main Menu from Credit
        mainMenuUI.SetActive(true);
        creditDesignMenuUI.SetActive(false);
    }

    // Pre-condition: creditDesignMenuUI is active and next page is assets credit
    public void CreditAssets()
    {
        // Go to Credit's Assets page
        creditAssetsMenuUI.SetActive(true);
        creditDesignMenuUI.SetActive(false);
    }

    public void CreditBackDesign()
    {
        // Go back to Design Credit page
        creditDesignMenuUI.SetActive(true);
        creditAssetsMenuUI.SetActive(false);
    }

    // Pre-condition: creditAssetsMenuUI is active and next page is audio credit
    public void CreditAudio()
    {
        // Go to Credit's Audio page
        creditAudioMenuUI.SetActive(true);
        creditAssetsMenuUI.SetActive(false);
    }

    public void CreditBackAssets()
    {
        // Go to Credit's Assets page
        creditAssetsMenuUI.SetActive(true);
        creditAudioMenuUI.SetActive(false);
    }

    public void Quit()
    {
        // Quit the game
        Application.Quit();
    }

    private void Start()
    {
        m_levelLoader = GameObject.FindObjectOfType<LevelLoader>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
