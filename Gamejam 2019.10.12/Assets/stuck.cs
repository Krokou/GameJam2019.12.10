using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stuck : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.position = new Vector3(4,12,1);
        }
    }

}
