using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathogen : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] locations;
    private Vector3 target;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float angularSpeed = 999999f;
    private GameObject organ;
    [SerializeField] private float deltaHealthLossRate = 0.5f;
    public HeartRate heartManager;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponentInParent<NavMeshAgent>();
        locations = GameObject.FindGameObjectsWithTag("Organ");
        int index = Random.Range(0, locations.Length);
        target = locations[index].transform.position;
        target = new Vector3(target.x, gameObject.transform.position.y, target.z);
        agent.speed = speed;
        agent.angularSpeed = angularSpeed;
        agent.SetDestination(target);
        organ = null;
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = speed * (heartManager.getCurrentRate() / 100);
    }

    private void OnTriggerEnter(Collider col) {
        GameObject other = col.gameObject;
        if (other.name == "Brain") {
            organ = other;
            organ.GetComponent<Organ>().healthLossRate += deltaHealthLossRate;
            agent.enabled = false;
        } else if (other.name == "Heart") {
            organ = other;
            organ.GetComponent<Heart>().healthLossRate += deltaHealthLossRate;
            agent.enabled = false;
        } else if (other.name == "Kidneys") {
            organ = other;
            organ.GetComponent<Kidneys>().healthLossRate += deltaHealthLossRate;
            agent.enabled = false;
        } else if (other.name == "Liver") {
            organ = other;
            organ.GetComponent<Liver>().healthLossRate += deltaHealthLossRate;
            agent.enabled = false;
        } else if (other.name == "Stomach") {
            organ = other;
            organ.GetComponent<Stomach>().healthLossRate += deltaHealthLossRate;
            agent.enabled = false;
        } else if (other.GetComponent<BloodCellProjectile>() != null) {
            agent.enabled = false;
            Kill();
        }
    }

    private void Kill() {
        if (organ != null) {
            if (organ.name == "Brain") {
                organ.GetComponent<Organ>().healthLossRate -= deltaHealthLossRate;
            } else if (organ.name == "Heart") {
                organ.GetComponent<Heart>().healthLossRate -= deltaHealthLossRate;
            } else if (organ.name == "Kidneys") {
                organ.GetComponent<Kidneys>().healthLossRate -= deltaHealthLossRate;
            } else if (organ.name == "Liver") {
                organ.GetComponent<Liver>().healthLossRate -= deltaHealthLossRate;
            } else if (organ.name == "Stomach") {
                organ.GetComponent<Stomach>().healthLossRate -= deltaHealthLossRate;
            }
        }
        Destroy(transform.parent.gameObject);
    }
    
}
