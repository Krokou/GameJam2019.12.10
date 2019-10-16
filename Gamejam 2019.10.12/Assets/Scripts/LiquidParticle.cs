using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticle : MonoBehaviour
{
    private const int PARTICLE_BORDER = 9;
    private static bool hasConfiguredPhysics = false;

    void Awake()
    {
        if (!hasConfiguredPhysics)
        {
            Physics.IgnoreLayerCollision(PARTICLE_BORDER, PARTICLE_BORDER);
            hasConfiguredPhysics = true;
        }
    }
}
