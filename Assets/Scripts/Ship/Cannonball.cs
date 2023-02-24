using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private BaseShip launcherShip;
    private GameObject launcherCannon;
    private Camera _camera;
    private Vector3 viewportVector;

    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        _camera = Camera.main;
    }

    private void Update()
    {
        BorderDetection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HealthController>() != null &&
            collision.gameObject != launcherCannon)
        {
            collision.GetComponent<HealthController>().TakeDamage(750);
            gameObject.SetActive(false);
            GetComponent<Rigidbody2D>().Sleep();

            if (collision.GetComponent<BaseShip>() is PlayerController)
            {
                gameController.AddScore(-250);
            }
            else
            {
                if (launcherShip is PlayerController)
                {
                    gameController.AddScore(500);
                }
                else
                {
                    gameController.AddScore(1000);
                }

                if (collision.GetComponent<HealthController>().CurrentHP <= 0)
                {
                    gameController.AddScore(1500);
                }
            }
        }
    }

    private void BorderDetection()
    {
        viewportVector = _camera.WorldToViewportPoint(transform.position);

        if (viewportVector.x >= 1.1f || viewportVector.x <= -0.1f ||
            viewportVector.y >= 1.1f || viewportVector.y <= -0.1f)
        {
            GetComponent<Rigidbody2D>().Sleep();
            gameObject.SetActive(false);
        }
    }

    public void SetLauncherCannon(GameObject gameObject)
    {
        launcherCannon = gameObject;
    }

    public void SetLauncherShip(BaseShip baseShip)
    {
        launcherShip = baseShip;
    }
}