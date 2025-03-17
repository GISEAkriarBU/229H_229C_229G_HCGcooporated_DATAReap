using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteExplosion : Meteorite
{
    private int damage;
    private float radius;

    public void SetDamage(int dmg, float rad)
    {
        damage = dmg;
        radius = rad;
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.5f);

        Collider[] enemies = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Enemy e = enemy.GetComponent<Enemy>();
                if (e != null)
                {
                    e.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }
}
