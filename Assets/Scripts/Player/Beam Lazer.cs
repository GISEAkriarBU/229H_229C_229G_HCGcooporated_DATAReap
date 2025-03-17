using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLazer : _Weapon
{
    public override void OnHitWith(Entity enemy) //polymorph
    { if (enemy is Enemy) { enemy.TakeDamage(this.Damage); Destroy(this.gameObject); } else if (enemy is not Enemy) { Destroy(this.gameObject); } }

    private void Start()
    {
       
    }

    private void FixedUpdate()
    {

        Move();
    }



    public override void Move()//polymorph
    {

        
    }
    
}
