using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public float diffForOK, diffForGood, diffForPerfect;
    public GameContoller controller;
    public float timeAliveBeforeSuddenSmoothieDeath;
    private Color customerColor;
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
        controller.makeNewCustomer();
        Destroy(this.gameObject);
    }

    public void Die()
    {
        controller.addPoints(-10);
        controller.makeNewCustomer();
        Destroy(gameObject);
    }


}
