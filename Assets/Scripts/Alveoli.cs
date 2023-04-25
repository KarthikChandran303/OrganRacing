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
        if (other.gameObject.layer == 6)
        {
            BloodCellManager.instance.AlveoliOxgenate();
        }
    }
}