using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Organ : MonoBehaviour
{
    public float maxHealth = 100;

    public float health;

    public float healthLossRate = 0.5f;

    public string organName = "Heart";

    private float bloodCellUseRate = 2f;

    [SerializeField] TMP_Text healthLabel;

    [SerializeField] Image healthBarSprite;

    [SerializeField] GameObject unoxyBloodCellPrefab;

    public Transform bloodCellSpawnLocation;

    public HeartRate heartManager;

    public enum Status { HEALTHY, DANGER, DYING };

    public Status status;

    AudioSource oxygenateSound;

    protected void Start()
    {
        health = maxHealth;
        GameObject soundObj = Instantiate(Resources.Load("OrganOxygenateSound") as GameObject, transform);
        soundObj.transform.parent = gameObject.transform;
        oxygenateSound = soundObj.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected void Update()
    {
        health -= healthLossRate * Time.deltaTime * (heartManager.getCurrentRate() / 100) * heartManager.heartDeteriorationFactor;
        if (health < 0)
            health = 0;

        healthLabel.text = organName + " Health: " + (int) health;

        healthBarSprite.fillAmount = health / maxHealth;

        HealthEffects();
    }

    protected virtual void HealthEffects()
    {
        if (health == 0)
        {
            if (status != Status.DYING)
            {
                status = Status.DYING;
                heartManager.OrganStatusChange(-1, 1);
            }
        }
        else if (health <= 30)
        {
            if (status == Status.DYING)
            {
                status = Status.DANGER;
                heartManager.OrganStatusChange(1, -1);
            }
            else if (status == Status.HEALTHY)
            {
                status = Status.DANGER;
                heartManager.OrganStatusChange(1, 0);
            }
        }
        else if (health > 30 && status != Status.HEALTHY)
        {
            status = Status.HEALTHY;
            heartManager.OrganStatusChange(-1, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        // BloodCellProjectile collides with this organ
        if (other.gameObject.layer == 9)
        {
            health += heartManager.bloodCellEffectiveness;

            Destroy(other.gameObject);

            Invoke("GenerateUnoxygenatedCell", bloodCellUseRate);

            oxygenateSound.Play();
        }
    }

    private void GenerateUnoxygenatedCell()
    {
        GameObject cell = Instantiate(unoxyBloodCellPrefab);
        cell.transform.position = bloodCellSpawnLocation.position;
    }
}
