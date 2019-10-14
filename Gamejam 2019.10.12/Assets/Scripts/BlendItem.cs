using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    NORMAL,
    MONEY,
}

[RequireComponent(typeof(Rigidbody2D))]
public class BlendItem : MonoBehaviour
{
    public ItemType itemType = ItemType.NORMAL;

    public Color blendedColor;
    public float blendIntensity, fillAmmount;
    public bool IsBeingBlended, IsPickedUp;
}
