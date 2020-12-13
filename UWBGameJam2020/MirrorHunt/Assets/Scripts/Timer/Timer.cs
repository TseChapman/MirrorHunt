using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject text;
    public TMPro.TextMeshProUGUI timerText;

    private float m_timer = 0;
    private bool m_isTimerActive = false;
    private GameOverMenu m_gameOverMenu;

    public void SetTimer(float countDownTimer)
    {
        // play an audio
        m_timer = countDownTimer;
        m_timer += 1f;
        m_isTimerActive = true;
       text.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Alarm");
    }

    public bool GetIsTimerActive() { return m_isTimerActive; }


    // Start is called before the first frame update
    void Start()
    {
        m_gameOverMenu = GameObject.FindObjectOfType<GameOverMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        DisplayTimer();
    }

    private void UpdateTimer()
    {
        if (m_isTimerActive)
        {
            if (m_timer > 0f)
            {
                m_timer -= Time.smoothDeltaTime;
            }
            else
            {
                // Timer is active, but the count down is done
                // Play an audio and then gameover
                FindObjectOfType<AudioManager>().Pause("Alarm");
                FindObjectOfType<AudioManager>().Play("Roar");
                m_gameOverMenu.SignalInstantGameOver();
                m_isTimerActive = false;
            }
        }
    }

    private void DisplayTimer()
    {
        m_timer = Mathf.Max(0f, m_timer);
        float minutes = Mathf.FloorToInt(m_timer / 60f);
        float seconds = Mathf.FloorToInt(m_timer % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
