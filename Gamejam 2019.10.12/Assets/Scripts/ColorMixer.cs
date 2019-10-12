using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMixer : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public struct Fluid
    {
        public Color color;
        public float weight;
    }

    public List<Fluid> colors;
    public SpriteRenderer[] sprites;
    private Color oldColor, mixedColor;
    private float deltaBlendTime = 1f, blendStartTime = 0f, totalWeight;
    private bool colorChanged = true;


    void Start()
    {
        totalWeight = 0f;
        foreach(Fluid color in colors)
        {
            totalWeight += color.weight;
        }
        mixedColor = mixColors(colors);
        blendStartTime = Time.time;
        deltaBlendTime = 1;
        oldColor = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateColor();   
    }

    public void addColor(Color color, float weight, float time = 0f)
    {
        totalWeight += weight;
        colorChanged = true;
        Fluid a = new Fluid();
        a.weight = weight;
        a.color = color;
        colors.Add(a);
        deltaBlendTime = time;
        blendStartTime = Time.time;
        mixedColor = mixColors(colors);
        oldColor = mixedColor;
       
    }
    void UpdateColor()
    {
        if (colorChanged)
        {
            if (deltaBlendTime > 0 && deltaBlendTime + blendStartTime > Time.time)
            {
                foreach(SpriteRenderer sprite in sprites)
                {
                    print("Hei");
                    sprite.color = new Color(
                        oldColor.r * (deltaBlendTime + blendStartTime - Time.time) / deltaBlendTime + mixedColor.r * (Time.time - blendStartTime) / deltaBlendTime,
                        oldColor.g * (deltaBlendTime + blendStartTime - Time.time) / deltaBlendTime + mixedColor.g * (Time.time - blendStartTime) / deltaBlendTime,
                        oldColor.b * (deltaBlendTime + blendStartTime - Time.time) / deltaBlendTime + mixedColor.b * (Time.time - blendStartTime) / deltaBlendTime
                    );
                }
            }
            else
            {
                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.color = mixedColor;
                    colorChanged = false;
                }

            }
        }   
    }


    Color mixColors(List<Fluid> colors)
    {
        Color mix = Color.black;
        foreach(Fluid color in colors)
        {
            mix.b += color.color.b*color.weight/ totalWeight;
            mix.g += color.color.g*color.weight / totalWeight;
            mix.r += color.color.r*color.weight / totalWeight;
        }
        return mix;
    }
}
