using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liver : Organ
{
    public GameObject cholesterolPositions;

    public GameObject cholesterolPrefab;

    private bool dying = false;

    Dictionary<Transform, GameObject> cholesterolInstances = new();

    public float cholesterolGenRate = 5f;

    protected new void Start()
    {
        base.Start();
    }

    protected override void HealthEffects()
    {
        if (health < 30 && !dying)
        {
            dying = true;
            Invoke("LiverDying", cholesterolGenRate);
        } else if (health >= 30 && dying)
        {
            dying = false;
            Invoke("CleanCholesterol", cholesterolGenRate);
        }
    }

    private void LiverDying()
    {
        Debug.Log("im dying bro!!!!");
        foreach (Transform chol in cholesterolPositions.transform)
        {
            // Generate a chloestrol blockage in some location that doesn't already contain one
            if (!cholesterolInstances.ContainsKey(chol))
            {
                Debug.Log("i have made a sick ball");
                GameObject cholesterol = Instantiate(cholesterolPrefab, chol.transform.position, chol.transform.rotation);
                cholesterolInstances.Add(chol, cholesterol);
                break;
            }
        }

        // Continue dying if dying
        if (dying)
        {
            Invoke("LiverDying", cholesterolGenRate);
        }
    }

    private void CleanCholesterol()
    {
        // Remove a cholestrol every 5 seconds
        foreach (Transform chol in cholesterolInstances.Keys)
        {
            Destroy(cholesterolInstances[chol]);
            cholesterolInstances.Remove(chol);
            break;
        }
        if (!dying && cholesterolInstances.Count > 0)
        {
            Invoke("CleanCholesterol", cholesterolGenRate);
        }
    }
}
