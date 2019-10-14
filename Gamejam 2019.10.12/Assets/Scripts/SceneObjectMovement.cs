using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectMovement : MonoBehaviour
{
    // Property, because this should not be set in the Unity inspector; it will be set by `SceneContainer`
    public SceneContainer Container { get; set; }

    public GameObject leftClone;
    public GameObject rightClone;

    private float backgroundWidth;

    private Rigidbody2D rb;

    private int deltaTime = 0;

    void Start()
    {
        backgroundWidth = GameContoller.INSTANCE.background.width;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-1000.0f, 1000.0f), 0.0f));
    }

    void FixedUpdate()
    {
        deltaTime += 1;

        if (deltaTime >= 100)
        {
            rb.AddForce(new Vector2(Random.Range(-1000.0f, 1000.0f), Random.Range(-50.0f, 500.0f)));
            deltaTime = 0;
        }

        float backgroundLeftEdgeX = -backgroundWidth / 2 + Container.transform.position.x;
        float backgroundRightEdgeX = backgroundWidth / 2 + Container.transform.position.x;
        if (transform.position.x < backgroundLeftEdgeX)
        {
            transform.position += new Vector3(backgroundWidth, 0, 0);
        }
        else if (transform.position.x > backgroundRightEdgeX)
        {
            transform.position += new Vector3(-backgroundWidth, 0, 0);
        }

        Quaternion rotation = transform.rotation;
        leftClone.transform.rotation = rotation;
        rightClone.transform.rotation = rotation;

        Vector3 position = transform.position;
        leftClone.transform.position = position + new Vector3(-backgroundWidth, 0, 0);
        rightClone.transform.position = position + new Vector3(backgroundWidth, 0, 0);
    }
}
