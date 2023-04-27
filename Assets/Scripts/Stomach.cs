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
        GameObject spawnPos = spawnablePositions[Random.Range(0, spawnablePositions.Count)];
        Spline spline = spawnPos.GetComponent<Spline>();
        CurveSample sample = spline.GetSample(Random.Range(0.25f, spline.nodes.Count - 1.25f));
        Vector3 randomPosition = spawnPos.transform.TransformPoint(sample.location);

        foreach (Transform a in acidInstances.Keys)
        {
            if (Vector3.Distance(randomPosition, a.transform.position) < minDistanceBetweenInstances)
            {
                Invoke("StomachDying", 0);
                return;
            }
        }

        GameObject acid = Instantiate(acidPrefab, randomPosition, Quaternion.identity);

        // raycast to track
        RaycastHit hit;
        if (Physics.Raycast(randomPosition + new Vector3(0, 1, 0), Vector3.down, out hit, 1 << 12))
        {
            Debug.DrawRay(randomPosition, hit.normal * 100, Color.magenta, 1000);
            acid.transform.localRotation = Quaternion.FromToRotation(acid.transform.up, hit.normal);
        }
        else
        {
            Debug.DrawRay(randomPosition, sample.up * 100, Color.green, 1000);
            acid.transform.localRotation = Quaternion.FromToRotation(acid.transform.up, sample.up);
        }

        //acid.transform.localPosition = new Vector3(Random.Range(acid.transform.position.x - 5, acid.transform.position.x + 5), acid.transform.position.y, acid.transform.position.z);
        acid.transform.localScale = new Vector3(acid.transform.localScale.x / 2, acid.transform.localScale.y / 2, acid.transform.localScale.z / 2);
        acid.transform.parent = transform;

        rumble.Play();
        acidInstances.Add(acid.transform, acid);
        // Continue dying if dying
        if (dying)
        {
            Invoke("StomachDying", acidGenRate);
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
