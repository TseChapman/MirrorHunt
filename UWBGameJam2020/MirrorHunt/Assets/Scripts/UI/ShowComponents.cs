using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowComponents : MonoBehaviour
{
    private GameOverMenu m_gameOverMenu;

    public void ShowOverComponent()
    {
        m_gameOverMenu.ShowComponents();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_gameOverMenu = GameObject.FindObjectOfType<GameOverMenu>();
    }
}
