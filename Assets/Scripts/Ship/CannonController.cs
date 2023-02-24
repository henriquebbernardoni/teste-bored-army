using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    private ShipAnimationControl animControl;

    private Transform frontCannon;
    private Transform[] leftCannons;
    private Transform[] rightCannons;

    private Rigidbody2D frontCB;
    private List<Rigidbody2D> leftCB;
    private List<Rigidbody2D> rightCB;

    private Cannonball[] cannonballs;

    private void Awake()
    {
        animControl = GetComponentInChildren<ShipAnimationControl>();

        frontCannon = transform.Find("Sprites/Other/CannonFront");
        List<Transform> tempLeftList = new();
        List<Transform> tempRightList = new();
        string tempPath = "Sprites/Other/CannonSide (";
        string tempFull;
        for (int i = 0; i < 3; i++)
        {
            tempFull = tempPath + (i + 1).ToString() + ")";
            tempLeftList.Add(transform.Find(tempFull));
            tempFull = tempPath + (i + 4).ToString() + ")";
            tempRightList.Add(transform.Find(tempFull));
        }
        leftCannons = tempLeftList.ToArray();
        rightCannons = tempRightList.ToArray();

        leftCB = new();
        rightCB = new();

        cannonballs = FindObjectsOfType<Cannonball>(true);
    }

    public void FrontCannonLaunch()
    {
        frontCB = FindFirstAvailableBall();
        frontCB.gameObject.SetActive(true);
        frontCB.transform.position = frontCannon.position;
        frontCB.velocity = transform.up * 10f;

        animControl.FrontCannonAnim();
    }

    public void SideCannonsLaunch()
    {
        leftCB.Clear();
        rightCB.Clear();

        for (int i = 0; i < rightCannons.Length; i++)
        {
            leftCB.Add(FindFirstAvailableBall());
            rightCB.Add(FindFirstAvailableBall());
        }

        if (leftCB.Count < leftCannons.Length || rightCB.Count < rightCannons.Length)
            return;

        for (int i = 0; i < leftCannons.Length; i++)
        {
            leftCB[i].gameObject.SetActive(true);
            leftCB[i].transform.position = leftCannons[i].position;
            leftCB[i].velocity = -transform.right * 10f;

            rightCB[i].gameObject.SetActive(true);
            rightCB[i].transform.position = rightCannons[i].position;
            rightCB[i].velocity = transform.right * 10f;
        }

        animControl.SideCannonAnim();
    }

    private Rigidbody2D FindFirstAvailableBall()
    {
        Rigidbody2D tempReturn = null;
        Rigidbody2D tempRB;

        for (int i = 0; i < cannonballs.Length; i++)
        {
            tempRB = cannonballs[i].GetComponent<Rigidbody2D>();

            if (!cannonballs[i].gameObject.activeInHierarchy &&
                frontCB != tempRB && !leftCB.Contains(tempRB) && !rightCB.Contains(tempRB))
            {
                tempReturn = tempRB;
                cannonballs[i].SetLauncherCannon(gameObject);
                cannonballs[i].SetLauncherShip(GetComponent<BaseShip>());
                break;
            }
        }

        return tempReturn;
    }
}