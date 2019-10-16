using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public float diffForOK, diffForGood, diffForPerfect;
    public GameContoller controller;
    public Color customerColor;
    public SpriteRenderer sweater;

    public GameObject dollar;
    public GameObject gold;
    public GameObject silver;
    public GameObject poop;

    private void Start()
    {
        sweater.color = customerColor;
    }
    bool colorIsCloseEnough(Color color1, Color color2, float diff)
    {
        if(
            Mathf.Abs(color1.r-color2.r) +
            Mathf.Abs(color1.g-color2.g) + 
            Mathf.Abs(color1.b-color2.b) < diff
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
            spawnMoney((int)(Random.value * 3 + 1), dollar);
            spawnMoney((int)(Random.value * 3 + 1), gold);
        }
        else if (colorIsCloseEnough(color, customerColor, diffForGood))
        {
            controller.addPoints(5);
            spawnMoney((int)(Random.value * 2), dollar);
            spawnMoney((int)(Random.value * 3 + 1), gold);
            spawnMoney((int)(Random.value * 3), silver);
        }
        else if (colorIsCloseEnough(color, customerColor, diffForOK))
        {
            controller.addPoints(2);
            spawnMoney((int)(Random.value * 2), gold);
            spawnMoney((int)(Random.value * 2 + 1), silver);
        }
        else
        {
            controller.addPoints(-10);
            spawnMoney(1, poop);
        }
        controller.customerPays(this);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void spawnMoney(int ammount, GameObject money)
    {
        if (ammount != 0)
        {
            for (int i = 0; i < ammount; i++)
            {
                Rigidbody2D spawnedItem = Instantiate(money, transform.position + Vector3.up*2, Quaternion.identity).GetComponent<Rigidbody2D>();
                spawnedItem.AddForce(new Vector2(Random.Range(50, 150), Random.Range(600,800)));
            }
        }

    }

}
