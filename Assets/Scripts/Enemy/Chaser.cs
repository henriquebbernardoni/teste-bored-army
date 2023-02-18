using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : EnemyBase
{
    protected override void EnemyBehavior()
    {
        RotateToPlayer();
        MoveToPlayer();
    }
}