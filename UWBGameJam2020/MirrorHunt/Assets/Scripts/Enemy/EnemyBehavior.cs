using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Animator animator;
    public Transform originalPos;
    public GameObject smokeEffectPrefab;
    public GameObject mainCam;
    public GameObject seeEnemyCam;
    public GameObject seeEnemyEffectCam;
    public GameObject jumpScareCam;
    public GameObject target;

    [SerializeField]
    [Range(0, 5)]
    public int maxHit = 3;

    private int m_numHit = 0;
    private bool m_isStun = false;
    private bool m_isDead = false;
    private EnemyPath m_enemyPath;
    private GameOverMenu m_gameOverMenu;
    private AudioManager m_audioManager;
    private float m_heartbeatTimer = 0f;
    private bool m_isHeartbeatActive = false;
    private bool m_isJumpScared = false;

    public void ReturnNormalTimeSpeed()
    {
        Time.timeScale = 1f;
    }

    public bool GetIsJumpScared() { return m_isJumpScared; }

    public void FootStep()
    {
        float dist = Vector3.Distance(originalPos.position, target.transform.position);
        if (dist < 20f)
            m_audioManager.Play("FootStep2");
    }

    // Teleport to a new node inside the map
    public void TeleportNewLocation()
    {
        //Debug.Log("Teleport");
        GameObject smoke = Instantiate(smokeEffectPrefab, originalPos.position, Quaternion.identity);
        Destroy(smoke, 1f);
        if (m_isDead is false)
            m_enemyPath.PickStartingNode();
    }

    public void IsStun()
    {
        m_isStun = (m_isStun is true) ? false : true;
    }

    public bool GetIsStun() { return m_isStun; }

    // True if enemy is dead
    public bool GetIsDead()
    {
        return m_isDead;
    }

    // See enemy in player camera
    public void TurnOnSeeEnemyCam()
    {
        seeEnemyCam.SetActive(true);
        seeEnemyEffectCam.SetActive(false);
        mainCam.SetActive(false);
    }

    public void TurnOnSeeEnemyEffect()
    {
        seeEnemyEffectCam.SetActive(true);
        seeEnemyCam.SetActive(false);
        mainCam.SetActive(false);
    }

    // Don't see enemy in player camera
    public void TurnOffSeeEnemyCam()
    {
        mainCam.SetActive(true);
        seeEnemyCam.SetActive(false);
        seeEnemyEffectCam.SetActive(false);
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_enemyPath = GameObject.FindObjectOfType<EnemyPath>();
        m_gameOverMenu = GameObject.FindObjectOfType<GameOverMenu>();
        m_audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (m_isDead)
            return;

        ReturnOriginalPos();
        Heartbeat();
    }

    // Check if the enemy is hit by a bullet
    private void OnTriggerEnter(Collider col)
    {
        if (m_isDead)
            return;
        //Debug.Log("hit");

        // If hit by a bullet
        if (col.transform.name == "45ACP Bullet_Head(Clone)")
        {
            // Destroy the bullet
            Destroy(col.transform.gameObject);
            m_audioManager.Play("BulletHit");
            TakeDamage();
            m_gameOverMenu.AddScore(30);
        }
        else if (col.transform.name == "Player" && m_isDead is false)
        {
            m_isJumpScared = true;
            m_gameOverMenu.AddScore(-10);
            m_audioManager.Play("Roar");
            jumpScareCam.SetActive(true);
            mainCam.SetActive(false);
            seeEnemyCam.SetActive(false);
            seeEnemyEffectCam.SetActive(false);
            TeleportNewLocation();
            StartCoroutine(EndJump());
        }
    }

    IEnumerator EndJump()
    {
        yield return new WaitForSeconds(2.0f);
        jumpScareCam.SetActive(false);
        m_isJumpScared = false;
        mainCam.SetActive(true);
    }

    // Take damage from bullet
    private void TakeDamage()
    {
        // Take Damage
        m_numHit++;
        // Look At Player
        transform.LookAt(target.transform);

        // turn slow motion
        Time.timeScale = 0.3f;
        m_audioManager.Play("DemonHurt");
        // play hurt animation
        animator.SetTrigger("Hurt");
        // If m_numHit is equal to maxHit, die and play finish game scene
        if (m_numHit >= maxHit)
        {
            m_gameOverMenu.AddScore(10);
            m_isDead = true;
            animator.SetBool("isDead", true);
            m_audioManager.Play("Roar");
            m_gameOverMenu.SignalGameOver();
        }
    }

    // Keep the enemy is original position
    private void ReturnOriginalPos()
    {
        Vector3 pos = originalPos.position;
        gameObject.transform.position = pos;
        Vector3 rotate = originalPos.eulerAngles;
        gameObject.transform.eulerAngles = rotate;
    }

    private void Heartbeat()
    {
        // Play Heartbeat faster and faster as the enemy and target get closer
        float dist = Vector3.Distance(originalPos.position, target.transform.position);
        m_isHeartbeatActive = (dist < 20f) ? true : false;
        if (dist < 20f && seeEnemyEffectCam.activeSelf is false && m_isStun is false && m_isJumpScared is false)
        {
            TurnOnSeeEnemyEffect();
        }
        else if (dist >= 20f && m_isStun is false && m_isJumpScared is false)
        { TurnOffSeeEnemyCam(); }
        m_heartbeatTimer -= Time.smoothDeltaTime;
        if (m_heartbeatTimer < 1f && m_isHeartbeatActive && m_audioManager.GetIsSoundPlaying("Heartbeat") is false && m_isDead is false)
        {
            m_heartbeatTimer = Mathf.Max(dist / 10,1f); 
            m_audioManager.Play("Heartbeat");
        }
    }
}
