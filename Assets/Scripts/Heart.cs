using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Organ
{
    protected override void HealthEffects()
    {
        if (health <= 0)
        {
            heartManager.heartDeteriorationFactor = 2;
        }
        else if (health < 30)
        {
            heartManager.heartDeteriorationFactor = 1.5f;
        }
        else
        {
            heartManager.heartDeteriorationFactor = 1;
        }
    }
}
