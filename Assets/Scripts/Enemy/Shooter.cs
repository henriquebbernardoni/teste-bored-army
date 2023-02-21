using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : EnemyBase
{
    private enum ChaserStates { CHASING, SHOOTING, DEACTIVATED }
    private ChaserStates currentState = ChaserStates.DEACTIVATED;

    private CannonController cannonController;
    private bool canShoot = true;

    protected override void Awake()
    {
        base.Awake();
        shipColor = SpriteDisplayer.ShipColor.BLACK;
        fullHPAmount = 4000;

        cannonController = GetComponent<CannonController>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestoreShip();
        }
    }

    protected override void EnemyBehavior()
    {
        switch (currentState)
        {
            case ChaserStates.CHASING:
                MoveToPlayer();

                if (Vector3.Distance(transform.position, player.position) <= 4f
                    && canShoot)
                {
                    StartCoroutine(Shoot());
                }
                break;
            case ChaserStates.SHOOTING:
                RotateToPlayer();
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

    private IEnumerator Shoot()
    {
        Agent.isStopped = true;
        currentState = ChaserStates.SHOOTING;
        canShoot = false;
        //rotationSpeed = 75f;

        yield return new WaitForSeconds(0.75f);

        cannonController.FrontCannonLaunch();
        Agent.isStopped = false;
        currentState = ChaserStates.CHASING;
        //rotationSpeed = 40f;

        yield return new WaitForSeconds(3f);

        canShoot = true;
    }
}