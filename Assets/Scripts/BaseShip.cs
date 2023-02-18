using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShip : MonoBehaviour
{
    protected SpriteDisplayer displayer;
    protected HealthController health;

    [SerializeField] protected SpriteDisplayer.ShipColor shipColor;

    protected virtual void Awake()
    {
        displayer = GetComponent<SpriteDisplayer>();
        health = GetComponent<HealthController>();
    }

    protected virtual void Start()
    {
        displayer.SetColor(shipColor);
        RestoreShip();
    }

    protected virtual void RestoreShip()
    {
        health.RestoreHP();
    }
}