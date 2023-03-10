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

    public float bloodCellValue = 10f;

    public string organName = "Heart";

    private float bloodCellUseRate = 2f;

    [SerializeField] TMP_Text healthLabel;

    [SerializeField] Image healthBarSprite;

    [SerializeField] GameObject unoxyBloodCellPrefab;

    public Transform bloodCellSpawnLocation;


    protected void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    protected void Update()
    {
        health -= healthLossRate * Time.deltaTime;

        healthLabel.text = organName + " Health: " + (int) health;

        healthBarSprite.fillAmount = health / maxHealth;

        HealthEffects();
    }

    protected virtual void HealthEffects()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        // BloodCellProjectile collides with this organ
        if (other.gameObject.layer == 9)
        {
            health += bloodCellValue;

            Destroy(other.gameObject);

            Invoke("GenerateUnoxygenatedCell", bloodCellUseRate);
        }
    }

    private void GenerateUnoxygenatedCell()
    {
        GameObject cell = Instantiate(unoxyBloodCellPrefab);
        cell.transform.position = bloodCellSpawnLocation.position;
    }
}
