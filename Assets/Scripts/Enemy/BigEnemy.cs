using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        Init(150); // Higher HP
        SetMovement(Vector3.back, 2f); // Moves forward slower
    }

    protected override int GetScoreValue()
    {
        return 50; // Score for killing big enemy
    }
}