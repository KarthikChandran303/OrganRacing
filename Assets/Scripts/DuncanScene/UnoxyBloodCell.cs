using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnoxyBloodCell : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Player picked up this blood cell
        if (other.gameObject.layer == 6)
        {
            if (BloodCellManager.instance.BloodCellCount() < 12)
            {
                BloodCellManager.instance.AddUnoxyBloodCell();
                Destroy(gameObject);
            }
        }
    }
}
