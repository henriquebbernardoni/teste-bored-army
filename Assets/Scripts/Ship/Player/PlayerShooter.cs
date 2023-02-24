using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private PlayerController playerController;

    private CannonController cannonController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        cannonController = GetComponent<CannonController>();
    }

    private void Update()
    {
        if (playerController.IsShipActive)
        {
            if (Input.GetMouseButtonUp(0))
            {
                cannonController.FrontCannonLaunch();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                cannonController.SideCannonsLaunch();
            }
        }
    }
}