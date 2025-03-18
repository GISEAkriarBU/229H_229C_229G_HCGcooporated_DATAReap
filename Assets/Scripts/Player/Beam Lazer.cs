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


    private void FixedUpdate()
    {

        delayTime += Time.deltaTime;
    }



    public override void Move()//polymorph
    {

        
    }
    
}
