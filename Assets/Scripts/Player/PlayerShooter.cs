using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private CannonController cannonController;

    private void Awake()
    {
        cannonController = GetComponent<CannonController>();
    }

    private void Update()
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