using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContainer : MonoBehaviour
{
    void Awake()
    {
        foreach (SceneObjectMovement sceneObject in GetComponentsInChildren<SceneObjectMovement>())
        {
            sceneObject.Container = this;
        }
    }
}
