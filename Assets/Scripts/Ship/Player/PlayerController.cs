using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseShip
{
    [SerializeField] private GameObject deathText;

    protected override void Start()
    {
        settingsController = FindObjectOfType<SettingsController>();
        shipColor = settingsController.PlayerColor;
        base.Start();
        RestoreShip();
    }

    public override void ShipDeath()
    {
        base.ShipDeath();
        gameController.MatchEnd();
        deathText.SetActive(false);
    }
}