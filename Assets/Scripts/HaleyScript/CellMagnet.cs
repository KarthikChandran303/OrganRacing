using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMagnet : MonoBehaviour
{
    [SerializeField] private float pullForce = 0.05f;
    private void OnTriggerStay(Collider other)
    {
        // Player picked up this blood cell
        if (other.gameObject.layer == 6) {
            transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, other.gameObject.transform.position, pullForce);
        }
    }
}
