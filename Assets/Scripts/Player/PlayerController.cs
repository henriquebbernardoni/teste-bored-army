using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseShip
{
    [SerializeField] private SpriteDisplayer.ShipColor playerColor;

    protected override void Awake()
    {
        base.Awake();
        shipColor = playerColor;
    }
}