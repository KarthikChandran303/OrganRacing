using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Organ : MonoBehaviour
{
    public float maxHealth = 100;

    public float health;

    public float healthLossRate = 0.05f;

    public float bloodCellValue = 10f;

    public string organName = "Heart";

    [SerializeField] TMP_Text healthLabel;


    private void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health -= healthLossRate * Time.deltaTime;

        healthLabel.text = name + " Health: " + (int) health;
    }

    private void OnTriggerEnter(Collider other)
    {
        float amount = (maxHealth - health) / bloodCellValue;

        // Player collides with this organ
        if (other.gameObject.layer == 6)
        {
            int bloodAmountUsed = BloodCellManager.instance.UseOxygen((int)amount);

            health += bloodCellValue * bloodAmountUsed;
        }
    }
}
