using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class CarGravity : MonoBehaviour
{
    public Rigidbody rb;
    public LayerMask layerMask;
    //public Spline spline;
    //[SerializeField] float dist;
    public float platformAngle = 22.5f;

    private void Start()
    {
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        //int layerMask = 1 << 12; // detect the track
        Vector3 updog = Vector3.up;

        //dist = ???;

        //updog = spline.GetSampleAtDistance(dist).up;

        RaycastHit hit;
        int hitLayer = 0;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 5, layerMask))
        {
            rb.useGravity = false;
            updog = hit.normal;
            hitLayer = hit.collider.gameObject.layer;
        }
        else 
        {
            rb.useGravity = true;    
        }

        if (hitLayer == 12 || (hitLayer == 15 && Vector3.Angle(updog, Vector3.up) <= platformAngle)) //12 == Track layer, 15 == Organ Floor (Platform) layer
        {
            if ((hit.point - transform.position).magnitude > 0.8f)
                rb.AddForce(-updog * 10000);
            else
                rb.AddForce(-updog * 50);

            var targetRotation = TurretLookRotation(transform.forward, updog);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Mathf.Abs(Quaternion.Angle(transform.rotation, targetRotation)) * 10 * Time.deltaTime);
        }
    }

    Quaternion TurretLookRotation(Vector3 approximateForward, Vector3 exactUp)
    {
        Quaternion zToUp = Quaternion.LookRotation(exactUp, -approximateForward);
        Quaternion yToz = Quaternion.Euler(90, 0, 0);
        return zToUp * yToz;
    }
}
