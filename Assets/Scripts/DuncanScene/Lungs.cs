using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lungs : MonoBehaviour
{
    public int bloodCellStorage = 0;

    public GameObject bloodCellPrefab;

    public Transform bloodCellSpawnLocation;

    private int generationRate = 7;

    private void OnTriggerEnter(Collider other)
    {
        // Player collides with this organ
        if (other.gameObject.layer == 6)
        {
            bloodCellStorage += BloodCellManager.instance.DeliverUnoxygenatedCells();

            if (bloodCellStorage > 0)
            {
                Invoke("GenerateOxygenatedBloodCell", generationRate);
            }
        }
    }

    private void GenerateOxygenatedBloodCell()
    {
        bloodCellStorage--;
        GameObject cell = Instantiate(bloodCellPrefab);
        cell.transform.position = bloodCellSpawnLocation.position;

        // Generate another blood cell if there are more to reoxygenate
        if (bloodCellStorage > 0)
        {
            Invoke("GenerateOxygenatedBloodCell", generationRate);
        }
    }
}
