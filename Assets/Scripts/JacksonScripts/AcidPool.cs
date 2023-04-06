using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    private Drive player;

    public AudioSource carCollisionSound;

    private void Start()
    {
        player = FindObjectOfType<Drive>();
        Debug.Log(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            player.ApplySpeedReduction();
            carCollisionSound.Play();
        }
    }
}