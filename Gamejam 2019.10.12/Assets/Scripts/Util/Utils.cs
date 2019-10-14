﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static T[] CreateFilledArray<T>(int num, T value)
    {
        T[] array = new T[num];
        for (int i = 0; i < array.Length; i++)
            array[i] = value;

        return array;
    }

    public static Vector2 RandomVector(float minMagnitude, float maxMagnitude)
    {
        return Random.onUnitSphere * Random.Range(minMagnitude, maxMagnitude);
    }

    public static bool ThrowDice(float chance)
    {
        return Random.value < chance;
    }
}
