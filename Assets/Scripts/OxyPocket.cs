using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxyPocket : MonoBehaviour
{
    public int bloodCellCount;

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.layer == 6) {
            Debug.Log("hey its me fry i cant come to the door right now im drunk");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Drive>().BounceImpact();


            if (bloodCellCount > 0)
            {
                BloodCellManager.instance.AddUnoxyBloodCell();
                bloodCellCount--;
            }
        }
    }
}
