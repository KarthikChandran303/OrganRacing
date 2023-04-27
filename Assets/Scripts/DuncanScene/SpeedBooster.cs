using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    private Drive player;

    public AudioSource boostSound;

    private void Start()
    {
        player = FindObjectOfType<Drive>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            player.ApplySpeedBoost();
            boostSound.Play();
        }
    }
}
