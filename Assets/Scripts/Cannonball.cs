using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private GameObject launcher;
    private Camera _camera;
    private Vector3 viewportVector;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        BorderDetection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HealthController>() != null &&
            collision.gameObject != launcher)
        {
            collision.GetComponent<HealthController>().TakeDamage(750);
            gameObject.SetActive(false);
            GetComponent<Rigidbody2D>().Sleep();
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

    public void SetLauncher(GameObject gameObject)
    {
        launcher = gameObject;
    }
}