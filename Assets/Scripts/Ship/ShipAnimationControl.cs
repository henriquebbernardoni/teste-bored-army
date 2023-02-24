using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimationControl : MonoBehaviour
{
    private Animation frontCannonAnim;
    private Animation sideCannonAnim1;
    private Animation sideCannonAnim2;
    private Animation shipDeathAnim;

    [SerializeField] private AnimationClip cannonShot;
    [SerializeField] private AnimationClip shipDeath;

    private void Awake()
    {
        frontCannonAnim = transform.Find("FrontCannonAnim").GetComponent<Animation>();
        sideCannonAnim1 = transform.Find("SideCannonAnim (1)").GetComponent<Animation>();
        sideCannonAnim2 = transform.Find("SideCannonAnim (2)").GetComponent<Animation>();
        shipDeathAnim = transform.Find("ShipExplosionAnim").GetComponent<Animation>();

        cannonShot.legacy = true;
        shipDeath.legacy = true;

        frontCannonAnim.clip = cannonShot;
        frontCannonAnim.AddClip(cannonShot, "cannonShot");
        sideCannonAnim1.clip = cannonShot;
        sideCannonAnim1.AddClip(cannonShot, "cannonShot");
        sideCannonAnim2.clip = cannonShot;
        sideCannonAnim2.AddClip(cannonShot, "cannonShot");
        shipDeathAnim.clip = shipDeath;
        shipDeathAnim.AddClip(shipDeath, "shipDeath");
    }

    public void FrontCannonAnim()
    {
        frontCannonAnim.Play("cannonShot");
    }

    public void SideCannonAnim()
    {
        sideCannonAnim1.Play("cannonShot");
        sideCannonAnim2.Play("cannonShot");
    }

    public void ShipDeathAnim()
    {
        shipDeathAnim.Play("shipDeath");
    }
}