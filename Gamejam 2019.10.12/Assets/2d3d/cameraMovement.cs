using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{

    public float rotationSpeed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0.0f, rotationSpeed, 0.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0.0f, -rotationSpeed, 0.0f);
        }
    }
}
