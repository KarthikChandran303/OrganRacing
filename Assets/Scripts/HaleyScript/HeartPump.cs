using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPump : MonoBehaviour
{
    [SerializeField] private float shrinkSize = 0.9f;
    private HeartRate manager;
    private float pumpRate;
    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        manager = gameObject.GetComponentInParent<Heart>().heartManager;
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        pumpRate = manager.getCurrentRate();
        //transform.localScale = new Vector3()
    }
}
