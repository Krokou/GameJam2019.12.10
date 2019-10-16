using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBackground : MonoBehaviour
{
    public float width { get; set; }

    void Awake()
    {
        Bounds backgroundBounds = GetComponent<Renderer>().bounds;
        width = backgroundBounds.size.x;
    }
}
