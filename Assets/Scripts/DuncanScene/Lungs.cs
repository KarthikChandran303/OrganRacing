using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lungs : MonoBehaviour
{
    public int bloodCellStorage = 0;

    public GameObject bloodCellPrefab;

    public Transform bloodCellSpawnLocation;

    private int generationRate = 2;

    private void Start()
    {
        InvokeRepeating("GenerateOxygenatedBloodCell", 0, generationRate);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player collides with this organ
        if (other.gameObject.layer == 6)
        {
            bloodCellStorage += BloodCellManager.instance.DeliverUnoxygenatedCells();
        }
    }

    private void GenerateOxygenatedBloodCell()
    {
        if (bloodCellStorage > 0)
        {
            bloodCellStorage--;
            GameObject cell = Instantiate(bloodCellPrefab);
            cell.transform.position = bloodCellSpawnLocation.position;
            Debug.Log("yeah!!!!!");
        }
    }
}
