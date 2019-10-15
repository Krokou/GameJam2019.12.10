using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SceneObjectMovement : MonoBehaviour
{
    public GameObject leftClone;
    public GameObject rightClone;

    public Sprite sprite;

    private Transform container;
    private float backgroundWidth;

    void Start()
    {
        container = GameContoller.INSTANCE._2DScene.transform;
        backgroundWidth = GameContoller.INSTANCE.background.width;
        sprite = GetComponent<SpriteRenderer>().sprite;

        leftClone = InstantiateDummy(sprite);
        rightClone = InstantiateDummy(sprite);
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
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

    private void OnDestroy()
    {
        Destroy(leftClone);
        Destroy(rightClone);
    }

    /*public static T InstantiateAsSceneObject<T>(T item, Vector3 spawnPosition) where T : MonoBehaviour
    {
        Transform blendItemContainer = GameContoller.INSTANCE.blendItemContainer;
        float backgroundWidth = GameContoller.INSTANCE.background.width;

        T spawnedItem = Instantiate(item, spawnPosition, Quaternion.identity);

        Transform parentObject = new GameObject(item.name).transform;
        parentObject.position = spawnPosition;
        parentObject.parent = blendItemContainer;
        spawnedItem.transform.parent = parentObject.transform;

        Transform dummyCopyLeft = InstantiateDummyCopy(spawnedItem.gameObject, -backgroundWidth, parentObject, "Left", typeof(BlendItem));
        Transform dummyCopyRight = InstantiateDummyCopy(spawnedItem.gameObject, backgroundWidth, parentObject, "Right", typeof(BlendItem));

        SceneObjectMovement sceneObjectComponent = spawnedItem.gameObject.AddComponent<SceneObjectMovement>();
        sceneObjectComponent.leftClone = dummyCopyLeft.gameObject;
        sceneObjectComponent.rightClone = dummyCopyRight.gameObject;

        return spawnedItem;
    }

    private static Transform InstantiateDummyCopy(GameObject fromObject, float xPos, Transform parentObject, string namePrefix, params Type[] removeComponents)
    {
        GameObject dummyCopy = Instantiate(fromObject, new Vector2(xPos, 0), Quaternion.identity, parentObject);
        dummyCopy.name = $"{namePrefix} dummy {fromObject.name}";

        foreach (Type type in removeComponents)
            Destroy(dummyCopy.GetComponent(type));

        Destroy(dummyCopy.GetComponent<Rigidbody2D>());
        return dummyCopy.transform;
    }*/

    private GameObject InstantiateDummy(Sprite s)
    {
        GameObject g = new GameObject();
        g.AddComponent<SpriteRenderer>();
        g.GetComponent<SpriteRenderer>().sprite = s;
        g.GetComponent<SpriteRenderer>().sortingOrder = 4;
        g.SetActive(true);
        g.transform.localScale = transform.localScale;
        g.AddComponent<HideRenderer>();
        

        return g;
    }
}
