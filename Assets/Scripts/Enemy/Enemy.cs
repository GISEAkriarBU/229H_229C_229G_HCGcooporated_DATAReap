using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private ConstantForce force;  // Constant force for movement

    protected virtual void Start()
    {
        force = GetComponent<ConstantForce>();
    }

    public void SetMovement(Vector3 direction, float power)
    {
        if (force != null)
        {
            force.force = direction * power;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(10);
            Destroy(other.gameObject);

            // Give player score
            Player player = FindAnyObjectByType <Player>();
            if (player != null)
            {
                player.AddScore(GetScoreValue());
            }
        }
    }

    protected virtual int GetScoreValue() { return 0; }
}