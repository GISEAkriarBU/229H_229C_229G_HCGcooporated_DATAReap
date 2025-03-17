using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        Init(50); // Normal enemy HP
        SetMovement(Vector3.back, 5f); // Moves forward faster
    }

    protected override int GetScoreValue()
    {
        return 10; // Score for killing normal enemy
    }
}

