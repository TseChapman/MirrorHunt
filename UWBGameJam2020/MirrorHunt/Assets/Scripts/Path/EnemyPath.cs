using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathMode { RANDOM = 0, A_STAR = 1, NUM_PATH_MODE = 2};

public class EnemyPath : MonoBehaviour
{
    public PathMode pathMode = PathMode.NUM_PATH_MODE;
    public GameObject enemy;
    public float nextActionSec = 4f;

    private float m_smoothTime = 1f;
    private Vector3 m_velocity = Vector3.zero;

    private Node[] m_nodes;
    private Node m_currentNode;
    private Node m_targetNode;
    private EnemyBehavior m_enemyBehavior;
    private float m_nextActionTime;

    public void PickStartingNode()
    {
        //Debug.Log("New node");
        int index = UnityEngine.Random.Range(0, m_nodes.Length);
        m_currentNode = m_nodes[index]; // Starting node
        m_targetNode = m_currentNode;
        StartPosition();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_enemyBehavior = GameObject.FindObjectOfType<EnemyBehavior>();
        m_nextActionTime = Time.time;
        m_nodes = GameObject.FindObjectsOfType<Node>();
        PickStartingNode();
    }

    private void FixedUpdate()
    {
        bool isValid = ValidCheck();
        if (!isValid)
            return;
        if (m_enemyBehavior.GetIsDead() is true)
            return;

        //Debug.Log("" + m_nextActionTime + ", " + Time.time);
        if (Time.time > m_nextActionTime)
        {
            //Debug.Log("Action");
            Action();
        }
        MoveEnemy();
        AnimateMovement();
        //Debug.Log(m_velocity);
    }

    // Place the enemy in the starting node position
    private void StartPosition()
    {
        enemy.transform.position = m_currentNode.GetNodePosition();
    }

    private void MoveEnemy()
    {
        if (m_targetNode != null && m_enemyBehavior.GetIsStun() is false)
        {
            enemy.transform.LookAt(m_targetNode.transform);
            enemy.transform.position = Vector3.SmoothDamp(enemy.transform.position, m_targetNode.GetNodePosition(), ref m_velocity, m_smoothTime);
            //Debug.Log("Move");
        }
    }

    private bool CheckVelocity()
    {
        if ((m_velocity.x > 0.3 || m_velocity.x < -0.3) || (m_velocity.z > 0.3 || m_velocity.z < -0.3))
            return true;
        return false;
    }

    private void AnimateMovement()
    {
        bool isWalk = CheckVelocity();
        //Debug.Log(m_velocity);
        //Debug.Log(isWalk);
        m_enemyBehavior.animator.SetBool("isWalk", isWalk);
    }

    private bool ValidCheck()
    {
        if (enemy == null)
            return false;
        if (pathMode == PathMode.NUM_PATH_MODE)
            return false;
        return true;
    }

    private void RandomAction()
    {
        Node[] neighbor = m_currentNode.GetNeighbors();
        int index = UnityEngine.Random.Range(0, neighbor.Length);
        m_targetNode = neighbor[index];
        m_currentNode = m_targetNode;
        MoveEnemy();
    }

    private void Action()
    {
        if (pathMode == PathMode.RANDOM)
            RandomAction();
        m_nextActionTime = Time.time + nextActionSec;
    }
}
