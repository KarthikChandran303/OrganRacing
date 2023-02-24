using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public GameObject bloodCellPrefab;

    public Transform bloodCellSpawnLocation;

    private int generationRate = 5;

    private void Start()
    {
        InvokeRepeating("GenerateBloodCell", 5, generationRate);
    }

    private void GenerateBloodCell()
    {
        GameObject cell = Instantiate(bloodCellPrefab);
        cell.transform.position = bloodCellSpawnLocation.position;
    }
}
