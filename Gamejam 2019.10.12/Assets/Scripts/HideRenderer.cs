using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRenderer : MonoBehaviour
{

    Transform cam;

    SpriteRenderer render;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        cam = GameContoller.INSTANCE._2DCamera.transform;
    }

    private void Update()
    {
        if (transform.position.x >= cam.position.x + 15 || transform.position.x <= cam.position.x - 15)
        {
            render.enabled = false;
        }
        else
        {
            render.enabled = true;
        }
    }
}
