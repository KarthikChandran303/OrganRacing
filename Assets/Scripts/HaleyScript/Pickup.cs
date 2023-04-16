using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float pullForce = 0.1f;
    [SerializeField] private float bobSpeed = 2.5f;
    [SerializeField] private float bobHeight = 0.25f;
    [SerializeField] private float rotationSpeed = 45f;
    private float yPos;
    private void Awake() {
        yPos = transform.parent.position.y;
    }
    private void FixedUpdate() {
        transform.parent.position = new Vector3(transform.parent.position.x, Mathf.Sin(Time.time * bobSpeed) * bobHeight + yPos, transform.parent.position.z);
        transform.parent.Rotate(Vector3.up * rotationSpeed * Time.fixedDeltaTime, Space.Self);
    }
    private void OnTriggerStay(Collider other)
    {
        // Player picked up this blood cell
        if (other.gameObject.layer == 6) {
            transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, other.gameObject.transform.position, pullForce);
        }
    }
}
