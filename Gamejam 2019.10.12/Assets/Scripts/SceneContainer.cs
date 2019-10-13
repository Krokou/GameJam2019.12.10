using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContainer : MonoBehaviour
{
    void Awake()
    {
        foreach (objectMovement sceneObject in GetComponentsInChildren<objectMovement>())
        {
            sceneObject.Container = this;
        }
    }
}
