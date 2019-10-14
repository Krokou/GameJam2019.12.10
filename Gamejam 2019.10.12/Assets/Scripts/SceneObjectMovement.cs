using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectMovement : MonoBehaviour
{
    public GameObject leftClone;
    public GameObject rightClone;

    private Transform container;
    private float backgroundWidth;

    private Rigidbody2D rb;

    private int deltaTime = 0;

    void Start()
    {
        container = GameContoller.INSTANCE._2DScene.transform;
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

        float backgroundLeftEdgeX = -backgroundWidth / 2 + container.position.x;
        float backgroundRightEdgeX = backgroundWidth / 2 + container.position.x;
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

    public static BlendItem InstantiateAsSceneObject(BlendItem item, Vector3 spawnPosition)
    {
        Transform blendItemContainer = GameContoller.INSTANCE.blendItemContainer;
        float backgroundWidth = GameContoller.INSTANCE.background.width;

        BlendItem spawnedItem = Instantiate(item, spawnPosition, Quaternion.identity);

        Transform parentObject = new GameObject(item.name).transform;
        parentObject.position = spawnPosition;
        parentObject.parent = blendItemContainer;
        spawnedItem.transform.parent = parentObject.transform;

        Transform dummyCopyLeft = InstantiateDummyCopy(spawnedItem, -backgroundWidth, parentObject, "Left");
        Transform dummyCopyRight = InstantiateDummyCopy(spawnedItem, backgroundWidth, parentObject, "Right");

        SceneObjectMovement sceneObjectComponent = spawnedItem.gameObject.AddComponent<SceneObjectMovement>();
        sceneObjectComponent.leftClone = dummyCopyLeft.gameObject;
        sceneObjectComponent.rightClone = dummyCopyRight.gameObject;

        return spawnedItem;
    }

    private static Transform InstantiateDummyCopy(BlendItem fromObject, float xPos, Transform parentObject, string namePrefix)
    {
        GameObject dummyCopy = Instantiate(fromObject.gameObject, new Vector2(xPos, 0), Quaternion.identity, parentObject);
        dummyCopy.name = $"{namePrefix} dummy {fromObject.name}";
        Destroy(dummyCopy.GetComponent<BlendItem>());
        Destroy(dummyCopy.GetComponent<Rigidbody2D>());
        return dummyCopy.transform;
    }
}
