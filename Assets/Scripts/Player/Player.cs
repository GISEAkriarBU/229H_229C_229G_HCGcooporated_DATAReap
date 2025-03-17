using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ShootAble
{
    [SerializeField]
    private Transform bulletSpawn;
    public Transform BulletSpawn { get { return bulletSpawn; } set { bulletSpawn = value; } }

    [SerializeField]
    private GameObject bullet;
    public GameObject Bullet { get { return bullet; } set { bullet = value; } }
    public float BulletTimer { get; set; }
    public float BulletWaitTime { get; set; }

    private int score = 0;
    public int Score
    {
        get { return score; }
        set { score = value; Debug.Log("Score: " + score); }
    }
    private void Update() { Shoot(); }
    void FixedUpdate() { BulletWaitTime += Time.deltaTime; }
    public void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && BulletWaitTime >= BulletTimer)
        {
            GameObject obj = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
            BeamLazer bEnergy = obj.GetComponent<BeamLazer>();
            bEnergy.Init(20, this);

            BulletWaitTime = 0;
        }
            if (Input.GetButtonDown("Fire2") && BulletWaitTime >= BulletTimer)
            {
                GameObject obj = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
                Meteorite Mtr = obj.GetComponent<Meteorite>();
                Mtr.Init(50, this);

                BulletWaitTime = 0;
            }
    }

    public void AddScore(int points)
    {
        Score += points;
    }
}
