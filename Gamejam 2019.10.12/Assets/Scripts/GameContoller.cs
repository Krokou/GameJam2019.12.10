using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameContoller : MonoBehaviour
{
    public static GameContoller INSTANCE;

    public Text text;

    public AudioClip Ratsong;
    public AudioClip Smoothies;
    public GameObject rat;

    public Transform _2DCamera;
    public Transform _2DScene;
    public SceneBackground background;
    public Transform blendItemContainer;
    public BlendItem[] groceries;
    public BlendItem milk;
    public BlendItem weed;
    public MoneyItem[] moneyItems;

    public int points = 0;
    public GameObject customerPrefab;
    private List<CustomerController> customers = new List<CustomerController>(11);
    public Color sweaterColor;
    public float spawnTime, bounceValue;
    private float lastSpawn = 0f;
    public Vector3 lineIncrements;
    public Vector3 spawnPoint;
    public float lifespan = 30;
    private float timeStart;

    void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        } else if (INSTANCE != this)
        {
            Debug.LogWarning("There's more than one GameContoller in the scene!");
            Destroy(gameObject);
        }
        
    }

    void Start()
    {
        Cursor.visible = false;
        makeNewCustomer();
        timeStart = Time.time;
        GetComponent<AudioSource>().clip = Smoothies;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = true;
    }

    private void Update()
    {
        text.text = "Score: " + points;
    }

    private void FixedUpdate()
    {
        if(spawnTime + lastSpawn < Time.time)
        {
            makeNewCustomer();
        }

        if(Time.time - timeStart > lifespan && customers.Count != 0)
        {
            timeStart = Time.time;
            customerLeave(customers[0]);
        }
    }

    public void addPoints(int points)
    {
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

        if (customers.Count == 10)
        {
            GameOver();
            lastSpawn = Time.time + 10000;
        }
    }

    public void customerLeave(CustomerController cust)
    {
        addPoints(-10);
        customers.Remove(cust);
        cust.Die();
        foreach(CustomerController i in customers)
        {
            print(i);
            i.transform.position -= lineIncrements;
        }
        timeStart = Time.time;
    }

    public void customerPays(CustomerController cust)
    {
        if (customers.Count > 0)
        {
            customers.Remove(cust);
            cust.Die();
        }
        foreach (CustomerController i in customers)
        {
            print(i);
            i.transform.position -= lineIncrements;
        }
        timeStart = Time.time;
    }

    public CustomerController currentCust()
    {
        if (customers.Count > 0)
        {
            return customers[0];
        }
        else return null;
    }

    public void GameOver()
    {
        print("Game Over");
        GetComponent<AudioSource>().clip = Ratsong;
        GetComponent<AudioSource>().Play();
        GameObject[] go = GameObject.FindGameObjectsWithTag("Blendable");
        foreach (GameObject g in go)
        {
            GameObject obj = g;
            Instantiate(rat, obj.transform.position, obj.transform.rotation);
            Destroy(obj);
        }
    }
}

