using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    private Drive player;
    //serialize for now
    [SerializeField] private bool isActive = false; 
    [SerializeField] private float defaultActiveTime = 5f;
    [SerializeField] private float activeTimer = 0f;

    public bool alwaysActive;

    public MeshRenderer boosterMeshRenderer;
    public Material activeMat;
    public Material inactiveMat;

    public AudioSource boostSound;

    //[SerializeField] private ProjectileAttractor attractor;

    private void Start()
    {
        player = FindObjectOfType<Drive>();
        if (alwaysActive)
        {
            boosterMeshRenderer.material = activeMat;
            //Debug.Log(boosterMeshRenderer.material.name);
        }
        else
        { 
            boosterMeshRenderer.material = inactiveMat;
        }
    }

    private void Update()
    {
        if (isActive)
        {
            if (activeTimer < defaultActiveTime)
            {
                activeTimer += Time.deltaTime;
            }
            else
            {
                SetInactive();
                activeTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && (isActive || alwaysActive))
        {
            player.ApplySpeedBoost();
            boostSound.Play();
        }
        if (other.gameObject.layer == 9 && !alwaysActive) {
            Destroy(other.gameObject);
            SetActive();
        }
    }

    private void SetActive()
    { 
        isActive = true;
        boosterMeshRenderer.material = activeMat;
    }

    private void SetInactive()
    { 
        isActive = false;
        boosterMeshRenderer.material = inactiveMat;
    }
}
