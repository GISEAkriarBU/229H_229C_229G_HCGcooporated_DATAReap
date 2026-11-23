using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Entity, ShootAble
{
    [Header("Spawn Transforms & Prefabs")]
    [SerializeField]
    private Transform bulletSpawn;
    public Transform BulletSpawn { get { return bulletSpawn; } set { bulletSpawn = value; } }

    [SerializeField]
    private Transform meteorSpawn;
    public Transform MeteorSpawn { get { return meteorSpawn; } set { meteorSpawn = value; } }

    [SerializeField]
    private GameObject bullet;
    public GameObject Bullet { get { return bullet; } set { bullet = value; } }

    [SerializeField]
    private GameObject meteor;
    public GameObject Meteor { get { return meteor; } set { meteor = value; } }

    [Header("Stress Test / Mass Spawn Settings")]
    [Tooltip("จำนวน Laser ที่จะเสกต่อการยิง 1 ครั้ง")]
    [SerializeField] private int bulletSpawnCount = 1;
    [Tooltip("ขอบเขตพื้นที่สุ่มกระจายตำแหน่งของ Laser (X, Y, Z)")]
    [SerializeField] private Vector3 bulletSpreadArea = new Vector3(2f, 1f, 0f);

    [Tooltip("จำนวน Meteor ที่จะเสกต่อการยิง 1 ครั้ง")]
    [SerializeField] private int meteorSpawnCount = 1;
    [Tooltip("ขอบเขตพื้นที่สุ่มกระจายตำแหน่งของ Meteor (X, Y, Z)")]
    [SerializeField] private Vector3 meteorSpreadArea = new Vector3(10f, 3f, 10f);

    [Tooltip("เปิดเพื่อให้กดคลิกค้างแล้วยิงรัวตาม Timer ได้ (ไม่ต้องกดย้ำทีละคลิก)")]
    [SerializeField] private bool holdToStressTest = true;

    private int currentHealth;
    private int maxHealth = 100;

    [field: SerializeField]
    public float BulletTimer { get; set; }
    [field: SerializeField]
    public float BulletWaitTime { get; set; }
    [field: SerializeField]
    public float MeteoriteTimer { get; set; }
    [field: SerializeField]
    public float MeteoriteWaitTime { get; set; }
    private Animator animator;

    public void Start()
    {
        SceneAnalyticsTrigger.MeteorUsing = 0;
        Init(100);
        Init(maxHealth);
        UpdateScoreUI();
    }

    [SerializeField] private Text scoreText;  // Assign this in the Inspector
    private int score = 0;

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            Debug.Log("Score: " + score);
            UpdateScoreUI(); // Update UI when score changes

            if (score >= 100000) // Check if the player reached the next level
            {
                LoadNextScene();
            }
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("WinAndCredit"); // Replace with actual scene name
    }

    private void Update() { Shoot(); }

    void FixedUpdate()
    {
        BulletWaitTime += Time.deltaTime;
        MeteoriteWaitTime += Time.deltaTime;
    }

    public void Shoot()
    {
        bool fire1Pressed = holdToStressTest ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");
        bool fire2Pressed = holdToStressTest ? Input.GetButton("Fire2") : Input.GetButtonDown("Fire2");

        // ยิง Laser (Fire1)
        if (fire1Pressed && BulletWaitTime >= BulletTimer)
        {
            if (animator != null)
            {
                animator.ResetTrigger("Attack");
                animator.SetTrigger("Attack");
            }

            int count = Mathf.Max(1, bulletSpawnCount);
            for (int i = 0; i < count; i++)
            {
                Vector3 spawnPos = GetRandomSpreadPosition(BulletSpawn, bulletSpreadArea, count == 1);
                GameObject obj = Instantiate(Bullet, spawnPos, BulletSpawn.rotation);
                BeamLazer bEnergy = obj.GetComponent<BeamLazer>();
                if (bEnergy != null)
                {
                    bEnergy.Init(100, this);
                }
            }

            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
            BulletWaitTime = 0;
        }

        // ยิง Meteor (Fire2)
        if (fire2Pressed && MeteoriteWaitTime >= MeteoriteTimer)
        {
            if (animator != null)
            {
                animator.CrossFade("SpecialAttack", 0.1f);
            }

            SceneAnalyticsTrigger.MeteorUsing++;

            int count = Mathf.Max(1, meteorSpawnCount);
            for (int i = 0; i < count; i++)
            {
                Vector3 spawnPos = GetRandomSpreadPosition(MeteorSpawn, meteorSpreadArea, count == 1);
                GameObject obj = Instantiate(Meteor, spawnPos, MeteorSpawn.rotation);
                Meteorite Mtr = obj.GetComponent<Meteorite>();
                if (Mtr != null)
                {
                    Mtr.Init(500, this);
                }
            }

            if (audioSource != null && meteorSound != null)
            {
                audioSource.PlayOneShot(meteorSound);
            }
            MeteoriteWaitTime = 0;
        }
    }

    // คำนวณตำแหน่งสุ่มในกรอบพื้นที่รอบจุด Spawn (อิงตามแกน Local ของจุด Spawn)
    private Vector3 GetRandomSpreadPosition(Transform spawnPoint, Vector3 area, bool exactCenter)
    {
        if (exactCenter) return spawnPoint.position;

        Vector3 localOffset = new Vector3(
            Random.Range(-area.x * 0.5f, area.x * 0.5f),
            Random.Range(-area.y * 0.5f, area.y * 0.5f),
            Random.Range(-area.z * 0.5f, area.z * 0.5f)
        );

        return spawnPoint.TransformPoint(localOffset);
    }

    // วาดกรอบ Gizmos ในหน้า Scene View เพื่อให้กะขนาดพื้นที่ Spawn ได้ง่าย
    private void OnDrawGizmosSelected()
    {
        if (BulletSpawn != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.matrix = BulletSpawn.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, bulletSpreadArea);
        }

        if (MeteorSpawn != null)
        {
            Gizmos.color = new Color(1f, 0.5f, 0f);
            Gizmos.matrix = MeteorSpawn.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, meteorSpreadArea);
        }
    }

    public void AddScore(int points)
    {
        Score += points;
        audioSource.PlayOneShot(scoreSound);
        audioSource.PlayOneShot(hitSound); // ดึง hitSound มาจากคลาส Entity ได้ปกติ[cite: 20]
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // If player collides with enemy[cite: 20]
        {
            Debug.Log("Player hit by Enemy! Game Over.");
            SceneManager.LoadScene("GameOverScene"); // Replace with actual scene name[cite: 20]
        }
    }

    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip meteorSound;
    [SerializeField] private AudioClip scoreSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // ดึง audioSource มาจากคลาส Entity ได้ปกติ[cite: 20]
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator is missing on Player!");
        }
    }
}