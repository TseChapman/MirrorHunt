using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform referenceTransform;
    public float collisionOffset = 0.2f;

    private Vector3 m_defaultPos;
    private Vector3 m_directionNormalized;
    private Transform m_parentTransform;
    private float m_defaultDistance;

    // Start is called before the first frame update
    void Start()
    {
        m_defaultPos = transform.localPosition;
        m_directionNormalized = m_defaultPos.normalized;
        m_parentTransform = transform.parent;
        m_defaultDistance = Vector3.Distance(m_defaultPos, Vector3.zero);
    }

    private void FixedUpdate()
    {
        Vector3 currentPos = m_defaultPos;
        RaycastHit hit;
        Vector3 dirTmp = m_parentTransform.TransformPoint(m_defaultPos) - referenceTransform.position;
        if (Physics.SphereCast(referenceTransform.position, collisionOffset, dirTmp, out hit, m_defaultDistance))
        {
            currentPos = (m_directionNormalized * (hit.distance - collisionOffset));
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, Time.deltaTime * 15f);
    }
}
