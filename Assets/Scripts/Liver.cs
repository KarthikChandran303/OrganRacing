using SplineMesh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liver : Organ
{
    [SerializeField] private List<GameObject> spawnablePositions;

    public GameObject cholesterolPositions;

    public GameObject cholesterolPrefab;

    private bool dying = false;

    Dictionary<Transform, GameObject> cholesterolInstances = new();

    public float cholesterolGenRate = 5f;

    [SerializeField] private float minDistanceBetweenInstances = 100f;

    protected new void Start()
    {
        base.Start();
    }

    protected override void HealthEffects()
    {
        base.HealthEffects();

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
        GameObject spawnPos = spawnablePositions[Random.Range(0, spawnablePositions.Count)];
        Spline spline = spawnPos.GetComponent<Spline>();
        CurveSample sample = spline.GetSample(Random.Range(0.25f, spline.nodes.Count - 1.25f));
        Vector3 randomPosition = spawnPos.transform.TransformPoint(sample.location) + new Vector3(0, 1, 0);

        foreach (Transform c in cholesterolInstances.Keys)
        {
            if (Vector3.Distance(randomPosition, c.transform.position) < minDistanceBetweenInstances)
            {
                Invoke("LiverDying", 0);
                return;
            }
        }

        GameObject chol = Instantiate(cholesterolPrefab, randomPosition, Quaternion.identity);

        // raycast to track
        RaycastHit hit;
        if (Physics.Raycast(randomPosition, Vector3.down, out hit, 1 << 12))
        {
            Debug.DrawRay(randomPosition, hit.normal * 100, Color.magenta, 1000);
            chol.transform.localRotation = Quaternion.FromToRotation(chol.transform.up, hit.normal);
        }
        else
        {
            Debug.DrawRay(randomPosition, sample.up * 100, Color.green, 1000);
            chol.transform.localRotation = Quaternion.FromToRotation(chol.transform.up, sample.up);
        }

        chol.transform.localScale = new Vector3(chol.transform.localScale.x / 2, chol.transform.localScale.y / 2, chol.transform.localScale.z / 2);
        chol.transform.parent = transform;

        cholesterolInstances.Add(chol.transform, chol);
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
            cholesterolInstances[chol].GetComponent<Cholesterol>().DestroyCholesterol();
            cholesterolInstances.Remove(chol);
            break;
        }
        if (!dying && cholesterolInstances.Count > 0)
        {
            Invoke("CleanCholesterol", cholesterolGenRate);
        }
    }
}
