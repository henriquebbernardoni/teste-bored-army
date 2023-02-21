using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseShip
{
    private EnemyController enemyController;

    [SerializeField] private SpriteDisplayer.ShipColor playerColor;

    protected override void Awake()
    {
        base.Awake();
        enemyController = FindObjectOfType<EnemyController>();
        shipColor = playerColor;
    }
}