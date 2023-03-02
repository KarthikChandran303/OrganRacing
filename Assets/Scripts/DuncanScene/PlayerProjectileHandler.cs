using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileHandler : MonoBehaviour
{
    [SerializeField] GameObject bloodCellProjectile;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (BloodCellManager.instance.oxyBloodCellCount > 0)
            {
                Instantiate(bloodCellProjectile, transform.position + new Vector3(0, 1f, 0), transform.rotation);
                BloodCellManager.instance.UseBloodCell();
            }
        }
    }
}
