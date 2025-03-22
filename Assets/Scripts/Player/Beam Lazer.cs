using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLazer : _Weapon
{
    public float delayTime;
    public override void OnHitWith(Entity enemy) //polymorph
    {
        if (enemy is Enemy)
        {
            enemy.TakeDamage(this.Damage);
            if (delayTime > 20) { Destroy(this.gameObject); }
        }
        else if (delayTime > 60) { Destroy(this.gameObject); }
    }

    void Start()
    {
        if (CompareTag("Bullet")) // Make sure this object is tagged "Meteorite"
        {
            Rigidbody rb = GetComponent<Rigidbody>();
        }
    }
    private void FixedUpdate()
    {

        delayTime += Time.deltaTime;
        Move();
    }

    public override void Move()//polymorph
    {

        Rigidbody rb = GetComponent<Rigidbody>();
        if (CompareTag("Bullet"))
        {
            if (rb != null)
            {
                rb.AddTorque(Vector3.forward * 10f); 
                rb.AddForce(Vector3.forward * 50f, ForceMode.Acceleration);
            }
        }
        
    }
    
}
