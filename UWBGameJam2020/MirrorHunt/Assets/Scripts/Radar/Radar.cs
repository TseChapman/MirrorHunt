using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject[] trackEnemy;
    public GameObject radarPrefab;
    public Transform helpTransform;
    public float switchDistance;
    public float trackDistance;

    private List<GameObject> m_radarObject;
    private List<GameObject> m_borderObject;
    private Timer m_timer;
    private bool m_isTimerOn = false;

    private void Start()
    {
        m_timer = GameObject.FindObjectOfType<Timer>();
        CreateRadarObject();
    }

    private void Update()
    {
        BorderObject();
    }

    private void CreateRadarObject()
    {
        m_radarObject = new List<GameObject>();
        m_borderObject = new List<GameObject>();
        foreach (GameObject obj in trackEnemy)
        {
            GameObject enemy = Instantiate(radarPrefab, obj.transform.position, Quaternion.identity) as GameObject;
            m_radarObject.Add(enemy);
            GameObject enemyBorder = Instantiate(radarPrefab, obj.transform.position, Quaternion.identity) as GameObject;
            m_borderObject.Add(enemyBorder);
        }
    }

    private void BorderObject()
    {
        m_isTimerOn = m_timer.GetIsTimerActive();
        for (int i = 0; i < m_radarObject.Count; i++)
        {
            float dist = Vector3.Distance(m_radarObject[i].transform.position, transform.position);
            if (dist > switchDistance)
            {
                // Switch to borderObject
                helpTransform.LookAt(m_radarObject[i].transform);
                m_borderObject[i].transform.position = transform.position + switchDistance * helpTransform.forward;
                m_radarObject[i].transform.position = trackEnemy[i].transform.position;
                m_borderObject[i].layer = LayerMask.NameToLayer("Radar");
                m_radarObject[i].layer = LayerMask.NameToLayer("RadarInvis");
            }
            else
            {
                // Switch back to radarObject
                m_radarObject[i].transform.position = trackEnemy[i].transform.position;
                m_borderObject[i].transform.position = trackEnemy[i].transform.position;
                m_borderObject[i].layer = LayerMask.NameToLayer("RadarInvis");
                m_radarObject[i].layer = LayerMask.NameToLayer("Radar");
            }

            
            if (dist < trackDistance || m_isTimerOn)
            {
                m_borderObject[i].SetActive(false);
                m_radarObject[i].SetActive(false);
            }
            else
            {
                m_borderObject[i].SetActive(true);
                m_radarObject[i].SetActive(true);
            }
        }
    }

}
