using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ShootAble 
{
    Transform BulletSpawn { get; set; }
    GameObject Bullet { get; set; }
    float BulletTimer { get; set; }
    float BulletWaitTime { get; set; }
    void Shoot();
}
