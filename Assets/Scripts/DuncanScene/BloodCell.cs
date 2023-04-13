using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCell : MonoBehaviour
{
    public GameObject minimapIcon;

    private void Start()
    {
        Instantiate(minimapIcon, transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player picked up this blood cell
        if (other.gameObject.layer == 6)
        {
            BloodCellManager.instance.AddBloodCell();
            Destroy(gameObject);
        }
    }
}
