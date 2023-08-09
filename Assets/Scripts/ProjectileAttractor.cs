using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttractor : MonoBehaviour
{
    [SerializeField] private float pullForce = 0.1f;

    [SerializeField] private List<GameObject> projectileTargets;

    private void Start()
    {
        projectileTargets = new List<GameObject>();
    }

    private void Update()
    {
        //Make sure there aren't any null targets left in the attract list (will happen when attracted objects are picked up)
        CullNullTargets();

        if (projectileTargets.Count > 0)
        {
            Attract();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if nearby object is a blood cell projectile and if so, add to attract target list
        if (other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<BloodCellProjectile>().SetAttracted(true);
            projectileTargets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if nearby object is a blood cell projectile and if so, remove from attract target list
        if (other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<BloodCellProjectile>().SetAttracted(false);
            projectileTargets.Remove(other.gameObject);
        }
    }

    private void Attract()
    {
        foreach (GameObject obj in projectileTargets)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, transform.position, pullForce);
        }
    }

    private void CullNullTargets()
    {
        for (int i = 0; i < projectileTargets.Count; i++)
        {
            if (projectileTargets[i] == null)
            {
                projectileTargets.RemoveAt(i);
            }
        }
    }
}
