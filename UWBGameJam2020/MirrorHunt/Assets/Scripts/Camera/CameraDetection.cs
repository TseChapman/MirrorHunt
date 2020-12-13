using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetection : MonoBehaviour
{
    public float detectDistance = 20f;
    public GameObject enemy;
    public GameObject[] mapTile1_Cameras = new GameObject[NUM_MAPTILE1_CAMERA];
    public GameObject[] mapTile2_Cameras = new GameObject[NUM_MAPTILE2_CAMERA];

    private const int NUM_MAPTILE1_CAMERA = 47;
    private const int NUM_MAPTILE2_CAMERA = 53;
    private float m_enemyDetectDist = 20f;

    // For testing Frame rate
    private int m_frameCounter = 0;
    private float m_timeCounter = 0f;
    private float m_lastFramerate = 0f;
    private float m_refreshTime = 0.5f;
    private float m_timer = 0f;

    // Update is called once per frame
    private void Update()
    {
        m_timer -= Time.smoothDeltaTime;
        ExamineCamDist();
        //DebugFramerate();
    }

    // Check if the camera is within a certain range from the character
    private void ExamineCamDist()
    {
        if (m_timer <= 0f)
        {
            CheckCameras(ref mapTile1_Cameras);
            CheckCameras(ref mapTile2_Cameras);
            m_timer = 1f;
        }
        
    }

    private void CheckCameras(ref GameObject[] camera)
    {
        for (int i = 0; i < camera.Length; i++)
        {
            if (camera[i] == null)
                continue;

            float dist = Vector3.Distance(this.gameObject.transform.position, camera[i].transform.position);
            //float enemyDist = Vector3.Distance(enemy.transform.position, cam.transform.position);
            if (dist > detectDistance)// && enemyDist > m_enemyDetectDist)
            {
                camera[i].SetActive(false);
            }
            else
            {
                camera[i].SetActive(true);
            }
        }
    }

    private void DebugFramerate()
    {
        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            m_lastFramerate = (float)m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0f;
        }
        Debug.Log(m_lastFramerate);
    }
}
