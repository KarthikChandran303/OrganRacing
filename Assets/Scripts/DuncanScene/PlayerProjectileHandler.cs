using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileHandler : MonoBehaviour
{
    [SerializeField] GameObject bloodCellProjectile;

    private bool shootHeld = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Shoot") > 0 && !shootHeld)
        {
            shootHeld = true;
            if (BloodCellManager.instance.oxyBloodCellCount > 0)
            {
                Instantiate(bloodCellProjectile, transform.position + new Vector3(0, 1f, 0), transform.rotation);
                BloodCellManager.instance.UseBloodCell();
            }
        } else if (Input.GetAxis("Shoot") == 0 && shootHeld)
        {
            shootHeld = false;
        }
    }
}
