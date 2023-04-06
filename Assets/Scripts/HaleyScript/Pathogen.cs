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
    [SerializeField] private int health = 3;
    private GameObject organ;
    [SerializeField] private float deltaHealthLossRate = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        locations = GameObject.FindGameObjectsWithTag("Organ");
        int index = Random.Range(0, locations.Length);
        target = locations[index].transform.position;
        target = new Vector3(target.x, gameObject.transform.position.y, target.z);
        agent.speed = speed;
        agent.SetDestination(target);
        organ = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            Kill();
        }
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.name == "Brain") {
            organ = other.gameObject;
            organ.GetComponent<Organ>().healthLossRate += deltaHealthLossRate;
        } else if (other.gameObject.name == "Heart") {
            organ = other.gameObject;
            organ.GetComponent<Heart>().healthLossRate += deltaHealthLossRate;
        } else if (other.gameObject.name == "Kidneys") {
            organ = other.gameObject;
            organ.GetComponent<Kidneys>().healthLossRate += deltaHealthLossRate;
        } else if (other.gameObject.name == "Liver") {
            organ = other.gameObject;
            organ.GetComponent<Liver>().healthLossRate += deltaHealthLossRate;
        } else if (other.gameObject.name == "Stomach") {
            organ = other.gameObject;
            organ.GetComponent<Stomach>().healthLossRate += deltaHealthLossRate;
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
        Destroy(gameObject);
    }
    
}
