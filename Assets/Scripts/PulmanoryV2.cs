using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulmanoryV2 : Organ
{
    public GameObject boosters;

    public GameObject deactivatedBoosters;

    [SerializeField] HealthBar myHealthBar;

    protected new void Update()
    {
        base.Update();

        myHealthBar.UpdateHealthBar(health / maxHealth);
    }

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
