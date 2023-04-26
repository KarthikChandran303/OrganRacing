using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class Stomach : Organ
{
    [SerializeField] private List<GameObject> spawnablePositions;

    public GameObject acidPrefab;

    private bool dying = false;

    Dictionary<Transform, GameObject> acidInstances = new();

    public float acidGenRate = 5f;

    private AudioSource rumble;

    [SerializeField] private float minDistanceBetweenInstances = 50f;

    protected void Start()
    {
        base.Start();
        rumble = GetComponent<AudioSource>();
    }

    protected override void HealthEffects()
    {
        base.HealthEffects();

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
        while (true) {
            GameObject spawnPos = spawnablePositions[Random.Range(0, spawnablePositions.Count)];
            Spline spline = spawnPos.GetComponent<Spline>();
            CurveSample sample = spline.GetSample(Random.Range(0.25f, spline.nodes.Count - 1.25f));
            Vector3 randomPosition = spawnPos.transform.TransformPoint(sample.location);
            GameObject acid = Instantiate(acidPrefab, randomPosition, Quaternion.identity);
            //acid.transform.localRotation = Quaternion.FromToRotation(acid.transform.up, sample.up);
            acid.transform.localPosition = new Vector3(Random.Range(acid.transform.position.x - 5, acid.transform.position.x + 5), acid.transform.position.y, acid.transform.position.z);
            acid.transform.localScale = new Vector3(acid.transform.localScale.x / 2, acid.transform.localScale.y / 2, acid.transform.localScale.z / 2);
            acid.transform.parent = transform;
            if (acidInstances.Keys.Count == 0) {
                rumble.Play();
                acidInstances.Add(acid.transform, acid);
                // Continue dying if dying
                if (dying)
                {
                    Invoke("StomachDying", acidGenRate);
                }
                return;
            }
            foreach (Transform a in acidInstances.Keys)
            {
                if (Vector3.Distance(acid.transform.position, a.transform.position) > minDistanceBetweenInstances) {
                    rumble.Play();
                    acidInstances.Add(acid.transform, acid);
                    // Continue dying if dying
                    if (dying)
                    {
                        Invoke("StomachDying", acidGenRate);
                    }
                    return;
                } else {
                    Destroy(acid);
                }
            }
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
