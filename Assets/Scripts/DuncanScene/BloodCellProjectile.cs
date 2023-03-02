using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCellProjectile : MonoBehaviour
{
    [SerializeField] GameObject bloodCell;

    Rigidbody rb;

    float speed = 50;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If this projectile collides with a wall, destroy it and create a blood cell pickup item.
        if (other.gameObject.layer == 0 || other.gameObject.layer == 10)
        {
            Instantiate(bloodCell, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
