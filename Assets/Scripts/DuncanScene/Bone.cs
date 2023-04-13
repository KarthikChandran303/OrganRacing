using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public GameObject minimapIcon;

    public GameObject bloodCellPrefab;

    public Transform bloodCellSpawnLocation;

    private int generationRate = 20;

    private void Start()
    {
        Instantiate(minimapIcon, transform);
        InvokeRepeating("GenerateBloodCell", generationRate, generationRate);
    }

    private void GenerateBloodCell()
    {
        GameObject cell = Instantiate(bloodCellPrefab);
        cell.transform.position = bloodCellSpawnLocation.position;
    }
}
