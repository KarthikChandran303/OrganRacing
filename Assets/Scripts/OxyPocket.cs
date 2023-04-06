using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxyPocket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.layer == 6) {
            Debug.Log("hey its me fry i cant come to the door right now im drunk");
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(-rb.velocity * 10000);

        }
    }
}
