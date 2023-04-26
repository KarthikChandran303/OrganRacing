using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnoxyBloodCell : MonoBehaviour
{
    [SerializeField] Pickup pickup;

    Transform spawnTransform;

    private void Start()
    {
        spawnTransform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player picked up this blood cell
        if (other.gameObject.layer == 6)
        {
            if (BloodCellManager.instance.BloodCellCount() < 12)
            {
                BloodCellManager.instance.AddUnoxyBloodCell();
                if (transform.parent)
                    BloodCellManager.instance.Pickup(spawnTransform);
                Destroy(gameObject);
            }
        }
    }

    public void SpawnFromOxyPocket()
    {
        Animator an = GetComponent<Animator>();
        pickup.enabled = false;
        an.enabled = true;
        an.Play("generate");
        Invoke("ReenablePickup", 2);
    }

    private void ReenablePickup()
    {
        pickup.yPos = transform.position.y;
        pickup.enabled = true;
        gameObject.transform.parent = null;
    }
}
