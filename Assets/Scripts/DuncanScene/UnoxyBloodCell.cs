using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnoxyBloodCell : MonoBehaviour
{
    [SerializeField] private float bobSpeed = 2.5f;
    [SerializeField] private float bobHeight = 0.25f;
    [SerializeField] private float rotationSpeed = 45f;
    private float yPos;
    private void Awake() {
        yPos = transform.position.y;
    }
    private void FixedUpdate() {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * bobSpeed) * bobHeight + yPos, transform.position.z);
        transform.Rotate(Vector3.up * rotationSpeed * Time.fixedDeltaTime, Space.Self);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Player picked up this blood cell
        if (other.gameObject.layer == 6)
        {
            if (BloodCellManager.instance.BloodCellCount() < 12)
            {
                BloodCellManager.instance.AddUnoxyBloodCell();
                Destroy(gameObject);
            }
        }
    }
}
