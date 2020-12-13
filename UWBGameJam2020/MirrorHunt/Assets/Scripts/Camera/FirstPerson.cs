using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    public GameObject camera;
    private float mouseSensitivity = 100f;
    private EnemyBehavior m_enemyBehavior;

    private float m_X_Rotation = 0f;
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_enemyBehavior = GameObject.FindObjectOfType<EnemyBehavior>();
    }

    // Update is called once per frame
    private void Update()
    {
        //UpdateCameraPosition();
        Control();
    }

    private void UpdateCameraPosition()
    {
        Vector3 position = gameObject.transform.position;
        camera.transform.position = position;
    }

    private void Control()
    {
        if (m_enemyBehavior.GetIsJumpScared() is true)
            return;

        // Source: Brackey's Youtube Video: First Person Movement in Unity - FPS Controller
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.smoothDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.smoothDeltaTime;

        m_X_Rotation -= mouseY;
        m_X_Rotation = Mathf.Clamp(m_X_Rotation, -90f, 90f);

        //camera.transform.localRotation = Quaternion.Euler(m_X_Rotation, 0f, 0f);
        gameObject.transform.Rotate(Vector3.up * mouseX);
    }
}
