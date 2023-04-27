using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alveoli : MonoBehaviour
{
    private Drive player;

    private void Start()
    {
        player = FindObjectOfType<Drive>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("alveolitrigger");
        if (other.gameObject.layer == 6)
        {
            Debug.Log("inside");
            BloodCellManager.instance.AlveoliOxgenate();
        }
    }
}