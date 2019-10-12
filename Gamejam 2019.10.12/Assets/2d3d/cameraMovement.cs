using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{

    public float cameraSpeed = 0.2f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(cameraSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-cameraSpeed, 0, 0);
        }

        if (transform.position.x > 12.5)
        {
            transform.position -= new Vector3(25, 0, 0);
        }
        else if (transform.position.x < -12.5)
        {
            transform.position += new Vector3(25, 0, 0);
        }
    }

}
