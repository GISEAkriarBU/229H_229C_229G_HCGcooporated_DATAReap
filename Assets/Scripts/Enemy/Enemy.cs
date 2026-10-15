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
        // เมื่อชนกระสุนของผู้เล่น
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
        // เมื่อหลุดออกนอกจอ (ชน Boundary) ให้ลบตัวศัตรูทิ้งทันที โดยไม่เรียกฟังก์ชัน Die() คะแนนจึงไม่เพิ่ม
        else if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}