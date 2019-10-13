﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderController : MonoBehaviour
{
    public BlendItem test;
    public float max_fill = 100;
    public float current_ammount = 0, timePerPercentage;

    public float max_y_coordinates, min_y_coordinates;

    public SpriteRenderer fillSprite;
    private ColorMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        this.mixer = GetComponent<ColorMixer>();
        blendItem(test);
    }

    void blendItem(BlendItem blenditem)
    {
        print(mixer.name);
        mixer.addColor(blenditem.blendedColor, blenditem.blendIntensity, blenditem.fillAmmount*timePerPercentage);
        this.current_ammount += blenditem.fillAmmount;
        this.fillSprite.transform.localPosition = new Vector3(0,(current_ammount * (max_y_coordinates - min_y_coordinates) / max_fill) + min_y_coordinates, 0);
        //Destroy(blenditem.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
