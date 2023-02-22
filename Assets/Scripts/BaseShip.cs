using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShip : MonoBehaviour
{
    protected SpriteDisplayer displayer;
    protected HealthController health;
    protected ShipAnimationControl animControl;
    private Collider2D thisCollider;
    
    [SerializeField] protected int fullHPAmount = 5000;
    protected SpriteDisplayer.ShipColor shipColor;

    public bool IsShipActive { get; protected set; }

    protected virtual void Awake()
    {
        displayer = GetComponent<SpriteDisplayer>();
        health = GetComponent<HealthController>();
        animControl = GetComponentInChildren<ShipAnimationControl>();
        thisCollider = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        displayer.SetColor(shipColor);
        health.SetFullHP(fullHPAmount);
        RestoreShip();
    }

    public virtual void RestoreShip()
    {
        health.RestoreHP();
        thisCollider.enabled = true;
        IsShipActive = true;
    }

    public virtual void ShipDeath()
    {
        thisCollider.enabled = false;
        IsShipActive = false;
        animControl.ShipDeathAnim();
    }
}