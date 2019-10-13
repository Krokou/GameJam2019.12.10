using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerOfMass : MonoBehaviour
{
    public Vector3 pivot;
    public Rigidbody rb;

    void Start()
    {
        pivot = new Vector3(0.0f, 0.0f, 0.0f);
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = pivot;
        rb.freezeRotation = true;
    }
}
