using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderController : MonoBehaviour
{

    public handController hand;
    public GameContoller controller;
    public GameObject lid;
    public float max_fill = 100;
    public float launchX;
    public float current_ammount = 0, timePerPercentage;

    public float max_y_coordinates, min_y_coordinates; //  for spriten som fyller opp blenderen

    public SpriteRenderer fillSprite;
    private ColorMixer mixer;
    private float timeLidded = 0f, deltaLidTime = 0.5f;
    private bool lidded;
    public GameObject lidPrefab; 
    
    void Start()
    {
        mixer = GetComponent<ColorMixer>();
    }

    private void FixedUpdate()
    {
        if (lidded) {
            if(timeLidded + deltaLidTime < Time.time)
            {
                unLid();
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("Hei" + collision.name);
        BlendItem item = collision.GetComponent<BlendItem>();
        if (collision.transform.parent != null)
        {
            item = collision.GetComponentInParent<BlendItem>();
        }
        if (item != null)
        {
            blendItem(item);
            hand.Release();
        }
        else if (collision.tag == "Lid" && timeLidded + 2.5f < Time.time && current_ammount != 0)
        {
            lidPrefab = collision.gameObject;
            lidPrefab.SetActive(false);
            putOnLid();
            hand.Release();
        }
    }
    void unLid()
    {
        if (controller.currentCust() != null)
        {
            current_ammount = 0;
            controller.currentCust().giveCustomerSmoothie(mixer.empty());
            mixer.empty();
            fillSprite.transform.localPosition = new Vector3(0, (current_ammount * (max_y_coordinates - min_y_coordinates) / max_fill) + min_y_coordinates, 0);
        }

        int launchDir = ((int)(Random.value * 2) * 2) - 1;
        lidPrefab.SetActive(true);
        lidPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        lidPrefab.transform.position = lid.transform.position;
        lidPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector3(launchDir*1000, 200, 0));

        
        lid.SetActive(false);
        lidded = false;
    }
    void putOnLid()
    {
        print("LIDON!");
        lid.gameObject.SetActive(true);
        timeLidded = Time.time;
        lidded = true;
    }

    void blendItem(BlendItem blenditem)
    {
        if(blenditem.fillAmmount + current_ammount <= max_fill)
        {
            print(mixer.name);
            mixer.addColor(blenditem.blendedColor, blenditem.blendIntensity, blenditem.fillAmmount * timePerPercentage);
            current_ammount += blenditem.fillAmmount;
            fillSprite.transform.localPosition = new Vector3(0, (current_ammount * (max_y_coordinates - min_y_coordinates) / max_fill) + min_y_coordinates, 0);
            Destroy(blenditem.gameObject);
        }
        else
        {
            blenditem.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
        }
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
