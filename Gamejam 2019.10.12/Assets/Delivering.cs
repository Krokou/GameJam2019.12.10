using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivering : MonoBehaviour
{
    float deliverTime = 10;
    float lastDeliver;

    public GameObject milkman;
    public GameObject grocerer;

    void Start()
    {
        lastDeliver = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (deliverTime + lastDeliver <= Time.time)
        {
            lastDeliver = Time.time;
            Instantiate(MakeDeliverer((int)(Random.value * 2)), transform.position + Vector3.down, Quaternion.identity);
        }
    }

    GameObject MakeDeliverer(int type)
    {
        if (type == 1)
        {
            return milkman;
        }
        else
        {
            return grocerer;
        }
    }
}
