using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadImage : MonoBehaviour
{
    private LevelLoader m_levelLoader;

    public void TurnOff()
    {
        m_levelLoader.TurnImageOff();
    }

    private void Start()
    {
        m_levelLoader = GameObject.FindObjectOfType<LevelLoader>();
    }
}
