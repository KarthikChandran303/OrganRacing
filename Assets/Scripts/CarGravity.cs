using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGravity : MonoBehaviour
{
    public Rigidbody rb;

    private void FixedUpdate()
    {
        int layerMask = 1 << 12; // detect the track

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            float dist = transform.position.y - hit.point.y;
                //rb.velocity = new Vector3(rb.velocity.x, -20, rb.velocity.z);
            rb.AddForce(-hit.normal * 5000);
            rb.gameObject.transform.rotation = Quaternion.Euler(Vector3.RotateTowards(rb.gameObject.transform.up, hit.normal, 1, 1));
        } else
        {
            rb.AddForce(Vector3.down * 5000);
        }
    }
}
