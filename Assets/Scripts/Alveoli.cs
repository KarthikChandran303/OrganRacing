using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alveoli : MonoBehaviour
{
    private Drive player;

    private void Start()
    {
        player = FindObjectOfType<Drive>();
        Debug.Log(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Debug.Log("Hit Aveoli");
        }
    }
}