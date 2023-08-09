using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    
    [SerializeField] private float bobSpeed = 2.5f;
    [SerializeField] private float bobHeight = 0.25f;
    [SerializeField] private float rotationSpeed = 45f;
    
    public float yPos;
    public bool bob = true;
    public ParticleSystem attractedVFX;

    private void Awake() {
        yPos = transform.position.y;
    }
    private void FixedUpdate() {
        if (bob) {
            transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * bobSpeed) * bobHeight + yPos, transform.position.z);
            transform.Rotate(Vector3.up * rotationSpeed * Time.fixedDeltaTime, Space.Self);
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
        // Player is near enough to this blood cell to attract it 
    //    if (other.gameObject.layer == 6) {
            //bob = false;
    //        transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, other.gameObject.transform.position, pullForce);
    //    }
    //}
    //private void OnTriggerExit(Collider other) {
    //    yPos = transform.parent.position.y;
    //    bob = true;
    //}
}
