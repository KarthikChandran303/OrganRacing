using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPump : MonoBehaviour
{
    [SerializeField] private float shrinkSize = 0.1f;
    public HeartRate manager;
    private float originalScale;
    // Start is called before the first frame update
    void Start()
    {
        //manager = gameObject.GetComponentInParent<Heart>().heartManager;
        originalScale = transform.localScale.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float pumpRate = manager.getCurrentRate() / 300;
        float scale = -Mathf.PingPong(Time.time * pumpRate * 10f * shrinkSize, shrinkSize) * originalScale + originalScale;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
