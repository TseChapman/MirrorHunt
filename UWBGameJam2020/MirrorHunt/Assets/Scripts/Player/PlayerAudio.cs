using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private PlayerMovement m_playerMovement;

    public void FootStep()
    {
        m_playerMovement.PlayFootStep();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }
}
