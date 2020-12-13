using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCount : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TMPtext;

    public int maxAmmo = 10;
    private int m_currentAmmo;
    private bool m_isThereAmmo = true;
    private Timer m_timer;
    private EnemyBehavior m_enemyBehavior;
    private bool m_isEnemyDead = false;

    public int GetCurrentAmmo() { return m_currentAmmo; }

    public void ConsumeAmmo()
    {
        m_currentAmmo = Mathf.Max(0, m_currentAmmo-1);
        //Debug.Log(m_currentAmmo);
        UpdateText();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_currentAmmo = maxAmmo;
        m_timer = GameObject.FindObjectOfType<Timer>();
        m_enemyBehavior = GameObject.FindObjectOfType<EnemyBehavior>();
        UpdateText();
    }

    private void Update()
    {
        CheckAmmo();
    }

    private void CheckAmmo()
    {
        if (m_currentAmmo <= 0)
        {
            if (m_isThereAmmo && m_enemyBehavior.GetIsDead() is false)
            {
                m_isThereAmmo = false;
                StartCoroutine(SetTimerCountDown());
            }
        }
    }

    IEnumerator SetTimerCountDown()
    {
        yield return new WaitForSeconds(2f);
        if (m_enemyBehavior.GetIsDead() is false)
        {
            m_timer.SetTimer(30f);
        }
    }

    private void UpdateText()
    {
        TMPtext.text = "X " + m_currentAmmo;
    }
}
