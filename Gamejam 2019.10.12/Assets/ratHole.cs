using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratHole : MonoBehaviour
{
    public GameObject rat;

    private float lastSpawn;
    public float spawnTime = 10;

    private void Start()
    {
        lastSpawn = Time.time;
    }
    void Update()
    {
       if(lastSpawn + spawnTime <= Time.time)
        {
            lastSpawn = Time.time;
            Rigidbody2D spawnedItem = Instantiate(rat, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            spawnedItem.AddForce(new Vector2(Random.Range(-300, 300), Random.Range(-100, 100)));
        } 
    }
}
