using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContoller : MonoBehaviour
{
    public int points = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void addPoints(int points) {
        this.points += points; 
    }

    public void makeNewCustomer() {

        Color newColor = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255);

    }
}
