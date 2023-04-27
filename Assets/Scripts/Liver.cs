using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class Liver : Organ
{
    public GameObject cholesterolPrefab;

    private bool dying = false;

    Dictionary<Transform, GameObject> cholesterolInstances = new();

    public float cholesterolGenRate = 5f;
    [SerializeField] private List<GameObject> spawnablePositions;
    [SerializeField] private float minDistanceBetweenInstances = 50f;
    [SerializeField] private int maxNumberOfSpawns = 50;

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
        if(cholesterolInstances.Count == maxNumberOfSpawns) {
            return;
        }
        Debug.Log("im dying bro!!!!");
        GameObject spawnPos = spawnablePositions[Random.Range(0, spawnablePositions.Count)];
        Spline spline = spawnPos.GetComponent<Spline>();
        CurveSample sample = spline.GetSample(Random.Range(0.25f, spline.nodes.Count - 1.25f));
        Vector3 randomPosition = spawnPos.transform.TransformPoint(sample.location);
        randomPosition = new Vector3(randomPosition.x, randomPosition.y + 8, randomPosition.z);
        foreach (Transform c in cholesterolInstances.Keys)
        {
            if (Vector3.Distance(randomPosition, c.transform.position) < minDistanceBetweenInstances) {
                Debug.Log("bye");
                //Invoke("LiverDying", 0);
                return;
            }
        }
        GameObject cholesterol = Instantiate(cholesterolPrefab, randomPosition, Quaternion.identity);
        RaycastHit hit;
        if (Physics.Raycast(randomPosition, Vector3.down, out hit, 1 << 12))
        {
            //Debug.DrawRay(randomPosition, hit.normal * 100, Color.magenta, 1000);
            cholesterol.transform.localRotation *= Quaternion.FromToRotation(cholesterol.transform.up, hit.normal);
            cholesterol.transform.position = new Vector3(hit.point.x, hit.point.y + 8, Random.Range(hit.point.z - 4, hit.point.z + 4));
        }
        else
        {
            //Debug.DrawRay(randomPosition, sample.up * 100, Color.green, 1000);
            cholesterol.transform.localRotation *= Quaternion.FromToRotation(cholesterol.transform.up, sample.up);
            cholesterol.transform.position = new Vector3(sample.location.x, sample.location.y, Random.Range(sample.location.z - 4, sample.location.z + 4));
        }
        if (Physics.Raycast(randomPosition, Vector3.down, out hit, 1 << 12))
        {
            cholesterol.transform.localRotation *= Quaternion.FromToRotation(cholesterol.transform.up, hit.normal);
            cholesterol.transform.position = new Vector3(hit.point.x, hit.point.y + 2, hit.point.z);
        }
        cholesterol.transform.parent = transform;
        cholesterolInstances.Add(cholesterol.transform, cholesterol);
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
