using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kidneys : Organ
{
    protected override void HealthEffects()
    {
        if (health <= 0)
        {
            heartManager.bloodCellEffectiveness = 6;
        }
        else if (health < 30)
        {
            heartManager.bloodCellEffectiveness = 8;
        }
        else
        {
            heartManager.bloodCellEffectiveness = 10;
        }
    }
}
