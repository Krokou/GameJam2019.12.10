using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum DeliveryType
{
    GROCERIES,
    MILK,
}

public class Deliverer : MonoBehaviour
{
    public DeliveryType deliveryType;

    public float spawnTime;
    public float lifeSpan = 5;

    public float weedChance = 0.1f;
    public float minSpawnForce = 100f;
    public float maxSpawnForce = 1000f;

    private Transform blendItemContainer;

    void Start()
    {
        blendItemContainer = GameContoller.INSTANCE.blendItemContainer;
        spawnTime = Time.time;
    }

    private void Update()
    {
        if (spawnTime + lifeSpan <= Time.time)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        BlendItem blendItem = col.GetComponent<BlendItem>();
        if (blendItem != null && blendItem.itemType == ItemType.MONEY)
        {
            int numItemsValue = col.GetComponent<MoneyItem>().numItemsValue;

            BlendItem[] itemsToSpawn;
            switch (deliveryType)
            {
                case DeliveryType.GROCERIES:
                    if (Utils.ThrowDice(weedChance))
                        itemsToSpawn = Utils.CreateFilledArray(numItemsValue, GameContoller.INSTANCE.weed);
                    else
                        itemsToSpawn = Utils.GetRandomArray(numItemsValue, GameContoller.INSTANCE.groceries);
                    break;

                case DeliveryType.MILK:
                    itemsToSpawn = Utils.CreateFilledArray(numItemsValue, GameContoller.INSTANCE.milk);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (BlendItem item in itemsToSpawn)
            {
                Rigidbody2D spawnedItem = Instantiate(item.gameObject, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                spawnedItem.AddForce(Utils.RandomVector(minSpawnForce, maxSpawnForce));
            }

            GameObject.Find("rightHand").GetComponent<handController>().Release();
            Destroy(blendItem.gameObject);
        }
    }
}
