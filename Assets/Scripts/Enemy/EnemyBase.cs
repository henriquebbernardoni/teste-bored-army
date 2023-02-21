using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : BaseShip
{
    protected Transform player;

    protected float movementSpeed = 1.75f;
    protected float rotationSpeed = 40f;

    private NavMeshPath currentPath;
    private Vector3 currentCorner;
    private Vector3 targetDirection;
    private float newAngle;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<PlayerController>().transform;
        currentPath = new NavMeshPath();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected virtual void FixedUpdate()
    {
        EnemyBehavior();
    }

    protected virtual void EnemyBehavior()
    {
    }

    //As fun��es a seguir emulam a movimenta��o do jogador como se fosse um agente de NavMesh.
    //Ap�s realizar um playtest e conferir que o NavMesh Agent n�o satisfaz �s minhas necessidades,
    //eu decidi criar minha pr�pria vers�o do c�digo que realiza a tarefa muito bem.
    protected void RotateToPlayer()
    {
        NavMesh.CalculatePath(transform.position, player.position, NavMesh.AllAreas, currentPath);
        currentCorner = currentPath.corners[1];
        currentCorner.z = 0;
        targetDirection = currentCorner - transform.position;
        newAngle = Vector3.SignedAngle(transform.up, targetDirection, Vector3.forward);
        if (Mathf.Abs(newAngle) >= 1f)
        {
            transform.Rotate(Mathf.Sign(newAngle) * Time.fixedDeltaTime * rotationSpeed * Vector3.forward);
        }
    }
    protected void MoveToPlayer()
    {
        if(Vector3.Distance(transform.position, currentCorner) >= 0.5f)
        {
            transform.Translate(Time.fixedDeltaTime * movementSpeed * Vector3.up);
            //Debug.Log(Time.fixedDeltaTime * movementSpeed * Vector3.up);
        }
    }
}