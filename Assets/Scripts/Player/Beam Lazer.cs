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
        if (CompareTag("Bullet") && rb != null)
        {
            // Apply Spin (Angular Velocity)
            Vector3 spinAxis = Vector3.forward; // Example: Spinning around the Z-axis
            float spinSpeed = 10f;
            rb.AddTorque(spinAxis * spinSpeed, ForceMode.Acceleration);

            // Apply Forward Motion
            rb.AddForce(Vector3.forward * 50f, ForceMode.Acceleration);

            // --- Magnus Effect ---
            Vector3 velocity = rb.linearVelocity;
            Vector3 angularVelocity = rb.angularVelocity;
            float magnusCoefficient = 0.05f; // Adjust based on simulation needs

            // Magnus force: perpendicular to both velocity and spin
            Vector3 magnusForce = magnusCoefficient * Vector3.Cross(angularVelocity, velocity);
            rb.AddForce(magnusForce, ForceMode.Acceleration);
        }
    }
    
}
