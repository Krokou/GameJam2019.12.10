using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public float diffForOK, diffForGood, diffForPerfect;
    public GameContoller controller;
    public Color customerColor;
    public SpriteRenderer sweater;

    public float minSpawnForce = 100f;
    public float maxSpawnForce = 1000f;

    private void Start()
    {
        sweater.color = customerColor;
    }

    bool colorIsCloseEnough(Color color1, Color color2, float diff)
    {
        if (
            Mathf.Abs(color1.r - color2.r) +
            Mathf.Abs(color1.g - color2.g) +
            Mathf.Abs(color1.b - color2.b) < diff
        )
        {
            return true;
        }

        return false;
    }

    public void giveCustomerSmoothie(Color color)
    {
        if (colorIsCloseEnough(color, customerColor, diffForPerfect))
        {
            controller.addPoints(10);
        }
        else if (colorIsCloseEnough(color, customerColor, diffForGood))
        {
            controller.addPoints(5);
        }
        else if (colorIsCloseEnough(color, customerColor, diffForOK))
        {
            controller.addPoints(2);
        }
        else
        {
            controller.addPoints(-10);
        }

        controller.customerLeave(this);
    }

    public void Die()
    {
        controller.addPoints(-10);
        Destroy(gameObject);
    }

    void Update()
    {
        // TODO: remove and replace with call from when the blender has been given to the customer
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MoneyItem[] itemsToSpawn = Utils.GetRandomArray(3, GameContoller.INSTANCE.moneyItems);
            foreach (MoneyItem item in itemsToSpawn)
            {
                Rigidbody2D spawnedItem = SceneObjectMovement.InstantiateAsSceneObject(item, transform.position).GetComponent<Rigidbody2D>();
                spawnedItem.AddForce(Utils.RandomVector(minSpawnForce, maxSpawnForce));
            }
        }
    }
}
