using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OxyPocket : MonoBehaviour
{
    public int bloodCellCount;

    [SerializeField] Material emptyMaterial;
    [SerializeField] Material bloodMaterial;

    bool bloodHaver = true;

    private void Update()
    {
        /*if (bloodCellCount > 0)
        {
            if (!bloodHaver)
            {
                bloodHaver = true;
                gameObject.GetComponent<Renderer>().material = bloodMaterial;
            }
        }
        else if (bloodCellCount == 0)
        {
            if (bloodHaver)
            {
                bloodHaver = false;
                gameObject.GetComponent<Renderer>().material = emptyMaterial;
            }
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.layer == 6)
        {
            Debug.Log("hey its me fry i cant come to the door right now im drunk");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Drive>().BounceImpact();


            if (bloodCellCount > 0 && BloodCellManager.instance.BloodCellCount() < 12)
            {
                BloodCellManager.instance.AddUnoxyBloodCell();
                bloodCellCount--;
            }
        }
    }
}
