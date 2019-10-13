using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderController : MonoBehaviour
{
  

    public GameContoller controller;
    public GameObject lid;
    public float max_fill = 100;
    public float launchX;
    public float current_ammount = 0, timePerPercentage;

    public float max_y_coordinates, min_y_coordinates;

    public SpriteRenderer fillSprite;
    private ColorMixer mixer;
    private float timeLidded = 0f, deltaLidTime = 0.5f;
    private bool lidded;
    public GameObject lidPrefab;
    // Start is called before the first frame update
    void Start()
    {
        this.mixer = GetComponent<ColorMixer>();
    }

    private void FixedUpdate()
    {
        if (this.lidded) {
            if(this.timeLidded + deltaLidTime < Time.time)
            {
                unLid();
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("Hei" + collision.name);
        BlendItem item = collision.GetComponent<BlendItem>();
        if (item != null)
        {
            blendItem(item);
        }
        else if (collision.tag == "Lid")
        {
            this.lidPrefab = collision.gameObject;
            lidPrefab.SetActive(false);
            putOnLid();
            
        }
    }
    void unLid()
    {
        //controller.currentCust().giveCustomerSmoothie(mixer.empty());
        lidPrefab.transform.position = this.transform.position + new Vector3(0, launchX, 0);
        lidPrefab.SetActive(true);
        lidPrefab.GetComponent<Rigidbody2D>().angularVelocity = 720;
        lidPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5, 5), 5);
        
        lid.SetActive(false);
        lidded = false;
    }
    void putOnLid()
    {
        print("LIDON!");
        lid.gameObject.SetActive(true);
        this.timeLidded = Time.time;
        this.lidded = true;
    }

    void blendItem(BlendItem blenditem)
    {
        print(mixer.name);
        mixer.addColor(blenditem.blendedColor, blenditem.blendIntensity, blenditem.fillAmmount*timePerPercentage);
        this.current_ammount += blenditem.fillAmmount;
        this.fillSprite.transform.localPosition = new Vector3(0,(current_ammount * (max_y_coordinates - min_y_coordinates) / max_fill) + min_y_coordinates, 0);
        Destroy(blenditem.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
