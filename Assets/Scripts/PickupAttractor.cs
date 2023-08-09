using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAttractor : MonoBehaviour
{

    [SerializeField] private float pullForce = 0.1f;
    [SerializeField] private bool inputAttract = false;
    private bool isAttracting = false;
    
    [SerializeField] private List<GameObject> pickupTargets;

    [SerializeField] private List<ParticleSystem> attractVFX; 

    private void Start()
    {
        pickupTargets = new List<GameObject>();
    }

    private void Update()
    {
        //Make sure there aren't any null targets left in the attract list (will happen when attracted objects are picked up)
        CullNullTargets();

        if (Input.GetButton("Attract"))
        {
            inputAttract = true;
            foreach (ParticleSystem fx in attractVFX)
            {
                fx.Play();
            }
        }
        else { 
            inputAttract = false;
            foreach (ParticleSystem fx in attractVFX)
            {
                fx.Stop();
            }
        }

        if (inputAttract && pickupTargets.Count > 0)
        {
            if (!isAttracting)
            {
                AttractStart();
            }
            else
            {
                Attract();
            }
        }
        else if (isAttracting)
        {
            AttractEnd();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Object Detected");
        // Check if nearby object is a pickup item and if so, add to attract target list
        if (other.gameObject.layer == 8)
        {
            pickupTargets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if nearby object is a pickup item and if so, remove from attract target list
        if (other.gameObject.layer == 8)
        {
            pickupTargets.Remove(other.gameObject);
        }
    }

    private void Attract()
    {
        foreach (GameObject obj in pickupTargets) {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, transform.position, pullForce * Time.deltaTime);
        }
    }

    private void AttractStart()
    {
        isAttracting = true;
        
        foreach (GameObject obj in pickupTargets)
        {
            obj.GetComponent<Pickup>().bob = false;
            obj.GetComponent<Pickup>().attractedVFX.Play();
        }
        
    }

    private void AttractEnd()
    {
        isAttracting = false;

        foreach (GameObject obj in pickupTargets)
        {
            obj.GetComponent<Pickup>().bob = true;
            obj.GetComponent<Pickup>().attractedVFX.Stop();
        }

    }

    private void CullNullTargets()
    {
        for (int i = 0; i < pickupTargets.Count; i++)
        {
            if (pickupTargets[i] == null)
            {
                pickupTargets.RemoveAt(i);
            }
        }
    }
}
