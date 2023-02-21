using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform _transform;
    private Camera _camera;

    private float rotationSpeed = 75f;
    private float movementSpeed = 3f;

    //private bool isRotating;
    //private int rotationDirection;

    private void Awake()
    {
        _transform = transform;
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        Rotator();
        Mover();
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
            if (!IsOnScreenBorder())
            {
                _transform.Translate(Time.fixedDeltaTime * movementSpeed * Vector3.up);
                //Debug.Log(Time.fixedDeltaTime * movementSpeed * Vector3.up);
            }
        }
    }

    private bool IsOnScreenBorder()
    {
        bool result = false;
        Vector3 bowPosition = _transform.position + _transform.up;
        Vector3 viewportVector = _camera.WorldToViewportPoint(bowPosition);

        if (viewportVector.x >= 1 || viewportVector.x <= 0 ||
            viewportVector.y >= 1 || viewportVector.y <= 0)
        {
            result = true;
        }

        return result;
    }
}