using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarGraphicRotate : MonoBehaviour
{
    private float m_rotationSpeed = 180f;
    // Update is called once per frame
    private void Update()
    {
        RotateRadar();
    }

    private void RotateRadar()
    {
        transform.eulerAngles += new Vector3(0, m_rotationSpeed * Time.smoothDeltaTime, 0);
    }
}
