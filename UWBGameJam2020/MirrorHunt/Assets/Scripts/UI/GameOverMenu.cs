using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject menuComponent;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI playScoreText;

    private Animator animator;
    private bool m_isGameOver = false;
    private int m_score = 0;
    private LevelLoader m_levelLoader;

    // Signal the script to turn on the gameover menu
    public void SignalGameOver() 
    {
        FindObjectOfType<AudioManager>().Pause("Alarm");
        m_isGameOver = true; 
    }

    // Signal the script to turn on gameover menu instantly
    public void SignalInstantGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverMenu.SetActive(true);
        m_isGameOver = true;
        animator.SetTrigger("Instant");
    }

    // Add score to the current score
    public void AddScore(int amount) { m_score += amount; }

    // Get the current score
    public int GetScore() { return m_score; }

    // Show the buttons and text of the Gameover menu
    public void ShowComponents()
    {
        menuComponent.SetActive(true);
        Time.timeScale = 0f;
    }

    // Go back to Main Menu
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        m_levelLoader.NextLevel();
        SceneManager.LoadScene("MainMenu");
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    private void Start()
    {
        animator = gameOverMenu.GetComponent<Animator>();
        m_levelLoader = GameObject.FindObjectOfType<LevelLoader>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateScore();
        if (m_isGameOver is true)
        {
            GameOver();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Update the current score of the player
    private void UpdateScore()
    {
        scoreText.text = "Score: " + m_score;
        playScoreText.text = "Score: " + m_score;
    }

    // Turn on the GamerOver menu
    private void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverMenu.SetActive(true);
    }
}
