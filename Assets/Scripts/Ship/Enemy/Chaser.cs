using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : EnemyBase
{
    private enum ChaserStates { CHASING, DEACTIVATED }
    private ChaserStates currentState = ChaserStates.DEACTIVATED;

    protected override void Awake()
    {
        base.Awake();
        shipColor = SpriteDisplayer.ShipColor.WHITE;
    }

    protected override void EnemyBehavior()
    {
        switch (currentState)
        {
            case ChaserStates.CHASING:
                MoveToPlayer();
                break;
            case ChaserStates.DEACTIVATED:
                break;
            default:
                break;
        }
    }

    public override void RestoreShip()
    {
        base.RestoreShip();
        currentState = ChaserStates.CHASING;
    }

    public override void ShipDeath()
    {
        base.ShipDeath();
        currentState = ChaserStates.DEACTIVATED;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState == ChaserStates.CHASING &&
            collision.GetComponent<PlayerController>() != null)
        {
            collision.GetComponent<HealthController>().TakeDamage(3000);
            health.TakeDamage(fullHPAmount);
            gameController.AddScore(-750);
        }
    }
}