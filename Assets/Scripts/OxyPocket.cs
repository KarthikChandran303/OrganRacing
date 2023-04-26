using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OxyPocket : MonoBehaviour
{
    public int bloodCellCount;

    [SerializeField] Material emptyMaterial;
    [SerializeField] Material bloodMaterial;

    [SerializeField] Transform bloodCellSpawn;
    [SerializeField] GameObject bloodCellPrefab;

    bool generatingBlood = false;
    float generateTime = 5;

    private void Update()
    {
        if (!generatingBlood && bloodCellCount > 0)
        {
            generatingBlood = true;
            Invoke("GenerateBloodCell", generateTime);
        }
    }

    private void Start()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        bloodCellSpawn.LookAt(new Vector3(playerPos.x, bloodCellSpawn.position.y, playerPos.z));

        GameObject go = Instantiate(bloodCellPrefab, bloodCellSpawn);
        go.GetComponent<UnoxyBloodCell>().SpawnFromOxyPocket();
    }

    private void GenerateBloodCell()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        bloodCellSpawn.LookAt(new Vector3(playerPos.x, bloodCellSpawn.position.y, playerPos.z));

        GameObject go = Instantiate(bloodCellPrefab, bloodCellSpawn);
        go.GetComponent<UnoxyBloodCell>().SpawnFromOxyPocket();

        bloodCellCount--;

        if (bloodCellCount > 0)
            Invoke("GenerateBloodCell", generateTime);
        else
            generatingBlood = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.layer == 6)
        {
            Debug.Log("hey its me fry i cant come to the door right now im drunk");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Drive>().BounceImpact();


/*            if (bloodCellCount > 0 && BloodCellManager.instance.BloodCellCount() < 12)
            {
                BloodCellManager.instance.AddUnoxyBloodCell();
                bloodCellCount--;
            }*/
        }
    }
}
