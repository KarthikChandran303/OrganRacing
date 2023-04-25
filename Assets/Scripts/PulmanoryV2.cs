using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulmanoryV2 : Organ
{
    public GameObject boosters;

    public GameObject deactivatedBoosters;

    protected override void HealthEffects()
    {
        if (health < 10) {
            boosters.SetActive(false);
            deactivatedBoosters.SetActive(true);
        }
        else {
            boosters.SetActive(true);
            deactivatedBoosters.SetActive(false);
        }
    }
}
