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
            Rigidbody rb = go.GetComponent<Rigidbody>();
            Vector3 vel = rb.velocity;
            rb.AddForce(-vel * 10000);

            if (bloodCellCount > 0)
            {
                BloodCellManager.instance.AddUnoxyBloodCell();
                bloodCellCount--;
            }
        }
    }
}
