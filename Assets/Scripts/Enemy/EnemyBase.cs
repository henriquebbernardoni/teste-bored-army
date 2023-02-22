using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : BaseShip
{
    protected Transform player;
    public NavMeshAgent Agent { get; private set; }

    protected float movementSpeed = 1.75f;
    protected float rotationSpeed = 90f;

    private float enemyAgentDistance;
    private Vector3 playerAgentPos;
    private Vector3 targetDirection;
    private float newAngle;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<PlayerController>().transform;
    }

    protected override void Start()
    {
        base.Start();
        Agent.speed = movementSpeed;
        Agent.angularSpeed = rotationSpeed;
    }

    protected virtual void FixedUpdate()
    {
        EnemyBehavior();
    }

    protected virtual void EnemyBehavior()
    {
    }

    public override void RestoreShip()
    {
        base.RestoreShip();
        Agent.enabled = true;
        Agent.isStopped = false;
    }

    public override void ShipDeath()
    {
        base.ShipDeath();
        Agent.isStopped = true;
        Agent.enabled = false;
    }

    protected void MoveToPlayer()
    {
        if (IsShipActive)
        {
            enemyAgentDistance = Agent.transform.position.z - transform.position.z;
            playerAgentPos = player.position + (enemyAgentDistance * Vector3.forward);

            Agent.SetDestination(playerAgentPos);
            transform.position = Agent.transform.position - (enemyAgentDistance * Vector3.forward);

            if (Agent.path.corners.Length > 1)
            {
                targetDirection = Agent.path.corners[1] - transform.position;
                newAngle = Vector3.SignedAngle(transform.up, targetDirection, Vector3.forward);
                if (Mathf.Abs(newAngle) >= 1f)
                {
                    transform.Rotate(Mathf.Sign(newAngle) * Time.fixedDeltaTime * rotationSpeed * Vector3.forward);
                }
            }
        }
    }
    protected void RotateToPlayer()
    {
        if (IsShipActive)
        {
            targetDirection = player.position - transform.position;
            newAngle = Vector3.SignedAngle(transform.up, targetDirection, Vector3.forward);
            if (Mathf.Abs(newAngle) >= 1f)
            {
                transform.Rotate(Mathf.Sign(newAngle) * Time.fixedDeltaTime * rotationSpeed * Vector3.forward);
            }
        }
    }

    public void SetUpAgent(NavMeshAgent newAgent)
    {
        Agent = newAgent;
    }
}