using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OccupyType { NONE = 0, PLAYER = 1, ENEMY = 2, NUM_OCCUPY_TYPE = 3};

public class Node : MonoBehaviour
{
    //public Transform nodePosition;
    public Node[] neighbors;

    private OccupyType m_currentOccupyType = OccupyType.NONE;

    public Vector3 GetNodePosition()
    {
        return gameObject.transform.position;
    }

    public Node[] GetNeighbors()
    {
        return neighbors;
    }
}
