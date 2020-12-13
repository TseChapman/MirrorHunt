using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform originalPos;
    public GameObject avatar;
    public Animator animator;
    public CharacterController controller;
    public Transform groundCheck;
    public Transform shootPosition;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float attackRate = 1f;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private Vector3 m_Velocity;
    private bool m_IsGrounded;
    private float m_nextAttackTime = 0f;
    private float m_defaultSpeed;
    private float m_shotPower = 1500f;
    private BulletCount m_bulletCount;
    private EnemyBehavior m_enemyBehavior;

    public void PlayFootStep()
    {
        FindObjectOfType<AudioManager>().Play("FootStep");
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_defaultSpeed = speed;
        m_bulletCount = GetComponent<BulletCount>();
        m_enemyBehavior = GameObject.FindObjectOfType<EnemyBehavior>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Control();
        ReturnOriginalPos();
    }

    private void Control()
    {
        m_IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (m_IsGrounded && m_Velocity.y < 0)
        {
            m_Velocity.y = -2f;
        }
        if (m_enemyBehavior.GetIsJumpScared() is true)
            return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Vertical", Math.Abs(z));

        if (Time.time >= m_nextAttackTime)
        {
            //Debug.Log("Reclaim Speed");
            speed = m_defaultSpeed;
            if (Input.GetButtonDown("Fire1"))
            {
                Shot();
            }
        }
        else
        {
            speed = 0f;
        }
        Vector3 direction = transform.right * x + transform.forward * z;
        controller.Move(direction * speed * Time.smoothDeltaTime);

        /*
        if (Input.GetButtonDown("Jump") && m_IsGrounded)
        {
            m_Velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        */
        //m_Velocity.y += gravity * Time.smoothDeltaTime;
        controller.Move(m_Velocity * Time.smoothDeltaTime);
    }

    private void Shot()
    {
        if (m_bulletCount.GetCurrentAmmo() < 1)
            return;
        //Debug.Log("Attack");
        // Play Gun shot Audio
        FindObjectOfType<AudioManager>().Play("GunShot");
        // Create the muzzle flash
        GameObject flash = Instantiate(muzzleFlashPrefab, shootPosition.position, shootPosition.rotation);
        Destroy(flash, 2f); // 2f = destroy timer

        // Create a bullet head
        GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, shootPosition.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(shootPosition.forward * m_shotPower);
        Destroy(bullet, 3f);

        m_bulletCount.ConsumeAmmo();
        animator.SetTrigger("Attack");
        m_nextAttackTime = Time.time + 1f / attackRate;
    }

    private void ReturnOriginalPos()
    {
        Vector3 pos = originalPos.position;
        avatar.transform.position = pos;
    }
}
