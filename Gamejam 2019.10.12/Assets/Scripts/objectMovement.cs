using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMovement : MonoBehaviour
{
    // Property, because this should not be set in the Unity inspector; it will be set by `SceneContainer`
    public SceneContainer Container { get; set; }

    public float backgroundWidth = 25;

    private Rigidbody2D rb;

    public int deltaTime = 0;

    public GameObject rightClone;
    public GameObject leftClone;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-1000.0f,1000.0f), 0.0f));
    }

    void FixedUpdate()
    {
        deltaTime += 1;

        if (deltaTime >= 100)
        {
            rb.AddForce(new Vector2(Random.Range(-1000.0f, 1000.0f), Random.Range(-50.0f, 500.0f)));
            deltaTime = 0;
        }

        float backgroundRightEdgeX = backgroundWidth / 2 + Container.transform.position.x;
        float backgroundLeftEdgeX = -backgroundWidth / 2 + Container.transform.position.x;
        if (transform.position.x > backgroundRightEdgeX)
        {
            transform.position = transform.position + new Vector3(-backgroundWidth, 0, 0);
        }
        else if (transform.position.x < backgroundLeftEdgeX)
        {
            transform.position = transform.position + new Vector3(backgroundWidth, 0, 0);
        }

        leftClone.transform.rotation = transform.rotation;
        rightClone.transform.rotation = transform.rotation;

        leftClone.transform.position = transform.position + new Vector3(-backgroundWidth, 0, 0);
        rightClone.transform.position = transform.position + new Vector3(backgroundWidth, 0, 0);
    }
}
