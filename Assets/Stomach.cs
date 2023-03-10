using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomach : Organ
{
    public GameObject acidPositions;

    public GameObject acidPrefab;

    private bool dying = false;

    Dictionary<Transform, GameObject> acidInstances = new();

    public float acidGenRate = 5f;

    protected override void HealthEffects()
    {
        if (health < 30 && !dying)
        {
            dying = true;
            Invoke("StomachDying", acidGenRate);
        }
        else if (health >= 30 && dying)
        {
            dying = false;
            Invoke("CleanAcid", acidGenRate);
        }
    }

    private void StomachDying()
    {
        Debug.Log("im dying bro (STOMACH STOMACH STOMACH STOMACH)!!!!");
        foreach (Transform a in acidPositions.transform)
        {
            // Generate a chloestrol blockage in some location that doesn't already contain one
            if (!acidInstances.ContainsKey(a))
            {
                Debug.Log("i have made an ACID POOL");
                GameObject acid = Instantiate(acidPrefab, a.transform.position, a.transform.rotation);
                acidInstances.Add(a, acid);
                break;
            }
        }

        // Continue dying if dying
        if (dying)
        {
            Invoke("LiverDying", acidGenRate);
        }
    }

    private void CleanAcid()
    {
        // Remove an acid every 5 seconds
        foreach (Transform a in acidInstances.Keys)
        {
            Destroy(acidInstances[a]);
            acidInstances.Remove(a);
            break;
        }
        if (!dying && acidInstances.Count > 0)
        {
            Invoke("CleanAcid", acidGenRate);
        }
    }
}