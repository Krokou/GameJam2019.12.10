using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContoller : MonoBehaviour
{
    public int points = 0;
    public GameObject customerPrefab;
    private List<CustomerController> customers = new List<CustomerController>();
    public Color sweaterColor;
    public float spawnTime, bounceValue;
    private float lastSpawn = 0f;
    public Vector3 lineIncrements;
    public Vector3 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void FixedUpdate()
    {
        if(spawnTime + lastSpawn < Time.time)
        {
            makeNewCustomer();
        }
    }
    public void addPoints(int points) {
        this.points += points; 
    }

    public void makeNewCustomer() {

        lastSpawn = Time.time;
        

        Color newColor = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        
        customerPrefab.GetComponent<CustomerController>().customerColor = newColor;
        customerPrefab.GetComponent<CustomerController>().controller = this;

        //print(spawnPoint);
        CustomerController newCust = Instantiate(customerPrefab, spawnPoint + (lineIncrements * customers.Count), transform.rotation).GetComponent<CustomerController>();
        customers.Add(newCust);
        
    }
    public void customerLeave(CustomerController cust)
    {
        customers.Remove(cust);
        Destroy(cust.gameObject);
        foreach(CustomerController i in customers)
        {
            i.transform.position -= lineIncrements;
        }
    }
    public CustomerController currentCust()
    {
        return customers[0];
    }
    public void GameOver()
    {

    }
}

