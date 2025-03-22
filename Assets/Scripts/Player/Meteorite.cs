using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : _Weapon
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
        if (CompareTag("Meteorite")) // Make sure this object is tagged "Meteorite"
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
        if (CompareTag("Meteorite")) // Only execute if this object is tagged "Meteorite"
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddTorque(Vector3.up * 50f, ForceMode.Acceleration); // Example: Spin effect
                rb.AddForce(Vector3.down * 5f, ForceMode.Acceleration);
                rb.AddForce(Vector3.forward * 10f, ForceMode.Acceleration);

            }
        }
    }
   

}
