using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : _Weapon
{
    public float delayTime;


    public override void OnHitWith(Entity enemy) //polymorph
    { if (enemy is Enemy) 
        { enemy.TakeDamage(this.Damage);
            if (delayTime > 20) { Destroy(this.gameObject); } 
        } 
        else if (delayTime > 60) { Destroy(this.gameObject); } 
    }

   
    private void FixedUpdate()
    {

        delayTime += Time.deltaTime;
        Move();

    }



    public override void Move()//polymorph
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddTorque(10f); // Adjust the value to control spin speed
        }

    }
}
