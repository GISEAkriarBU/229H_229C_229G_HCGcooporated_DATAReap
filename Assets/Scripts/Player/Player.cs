using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : Entity, ShootAble
{
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


    public void Start()
    {
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

            if (score >= 1000) // Check if the player reached the next level
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
    void FixedUpdate() { BulletWaitTime += Time.deltaTime; MeteoriteWaitTime += Time.deltaTime; }
    public void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && BulletWaitTime >= BulletTimer)
        {
            GameObject obj = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
            BeamLazer bEnergy = obj.GetComponent<BeamLazer>();
            bEnergy.Init(100, this);

            audioSource.PlayOneShot(shootSound);
            BulletWaitTime = 0;
        }

        if (Input.GetButtonDown("Fire2") && MeteoriteWaitTime >= MeteoriteTimer)
        {
            GameObject obj = Instantiate(Meteor, MeteorSpawn.position, MeteorSpawn.rotation);
            Meteorite Mtr = obj.GetComponent<Meteorite>();
            Mtr.Init(500, this);

            audioSource.PlayOneShot(meteorSound);
            MeteoriteWaitTime = 0;
        }
    }

    public void AddScore(int points)
    {
        Score += points;
        audioSource.PlayOneShot(scoreSound);
        audioSource.PlayOneShot(hitSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // If player collides with enemy
        {
            Debug.Log("Player hit by Enemy! Game Over.");
            SceneManager.LoadScene ("GameOverScene"); // Replace with actual scene name
        }
    }
    
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip meteorSound;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip hitSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


}

