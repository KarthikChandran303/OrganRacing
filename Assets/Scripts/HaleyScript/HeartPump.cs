using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPump : MonoBehaviour
{
    [SerializeField] private float shrinkSize = 0.1f;
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
    void FixedUpdate()
    {
        pumpRate = manager.getCurrentRate() / 300;
        transform.localScale = new Vector3(-Mathf.PingPong(Time.time * pumpRate, shrinkSize) * scale + scale, -Mathf.PingPong(Time.time * pumpRate, shrinkSize) * scale + scale, -Mathf.PingPong(Time.time * pumpRate, shrinkSize) * scale + scale);
        
    }
}
