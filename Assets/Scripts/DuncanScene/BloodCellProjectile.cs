using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCellProjectile : MonoBehaviour
{
    public HeartRate heartManager;

    [SerializeField] GameObject bloodCell;

    Rigidbody rb;

    float speed = 50;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        heartManager = GameObject.FindGameObjectWithTag("HeartManager").GetComponent<HeartRate>();
    }

    // Update is called once per frame
    void Update()
    {   
        float heartSpeed = heartManager.getCurrentRate() / 100;
        rb.velocity = transform.forward * speed * heartSpeed * 1.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If this projectile collides with a wall, destroy it and create a blood cell pickup item.
        if (other.gameObject.layer == 0 || other.gameObject.layer == 10 || other.gameObject.layer == 12)
        {
            Instantiate(bloodCell, transform.position, transform.rotation);
            Destroy(gameObject);
        } else if (other.gameObject.layer == 12) {
            Destroy(gameObject);
        }
    }
}
