using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alveoli : MonoBehaviour
{
    private Drive player;

    private float collideTime;

    private void Start()
    {
        player = FindObjectOfType<Drive>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("alveolitrigger");
        if (other.gameObject.layer == 6)
        {
            BloodCellManager.instance.AlveoliOxgenateOne();
        }
    }

    private void OnTriggerStay(Collider other) {
        collideTime = collideTime + Time.deltaTime;
        if (other.gameObject.layer == 6) {
            if (collideTime > 0.25) {
                BloodCellManager.instance.AlveoliOxgenate();
                collideTime = 0;
            }
        }
    }
}