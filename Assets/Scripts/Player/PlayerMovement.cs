using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController playerController;

    private Transform _transform;
    private Camera _camera;

    private float rotationSpeed = 75f;
    private float movementSpeed = 3f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        _transform = transform;
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (playerController.IsShipActive)
        {
            Rotator();
            Mover();
        }

        /*RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider)
            Debug.Log(hit.collider.name);*/
    }

    private void Rotator()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            float axisValue = -Input.GetAxisRaw("Horizontal");

            _transform.Rotate(axisValue * Time.fixedDeltaTime * rotationSpeed * Vector3.forward);
        }
    }

    private void Mover()
    {
        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            if (!IsOnScreenBorder() && !ObstacleDetector())
            {
                _transform.Translate(Time.fixedDeltaTime * movementSpeed * Vector3.up);
            }
        }
    }

    private bool IsOnScreenBorder()
    {
        bool tempReturn = false;
        Vector3 bowPosition = _transform.position + _transform.up;
        Vector3 viewportVector = _camera.WorldToViewportPoint(bowPosition);

        if (viewportVector.x >= 1 || viewportVector.x <= 0 ||
            viewportVector.y >= 1 || viewportVector.y <= 0)
        {
            tempReturn = true;
        }

        return tempReturn;
    }

    private bool ObstacleDetector()
    {
        bool tempReturn = false;
        Vector3 bowPosition = _transform.position + _transform.up;

        RaycastHit2D hit = Physics2D.Raycast(bowPosition, _transform.up, 1f);
        if (hit.collider && hit.collider.GetComponent<Tilemap>())
        {
            tempReturn = true;
        }

        return tempReturn;
    }
}