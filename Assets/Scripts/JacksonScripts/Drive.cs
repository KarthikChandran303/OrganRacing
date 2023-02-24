using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{

    public Rigidbody sphere;
    public float forwardAccel = 8f;
    public float reverseAccel = 4f;
    public float maxSpeed = 50f;
    public float turnStrength = 18;
    public float timer = 0;
    public float speedTimer = 0;
    bool isSpeedBoosted = false;
    bool drifting = false;
    private float speedInput;
    private float turnInput;

    // Start is called before the first frame update
    void Start()
    {
       sphere.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0f;
        if (!Input.GetKey("space")) { 
            if (Input.GetAxis("Vertical") > 0) {
                speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
            } 
            else if (Input.GetAxis("Vertical") < 0) {
                speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
            } 
            turnInput = Input.GetAxis("Horizontal");
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3 (0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));

            transform.position = sphere.transform.position;
            drifting = false;
            if (timer >= 2) {
                isSpeedBoosted = true;
            }
            timer = 0;
        }
        else {
            if (Input.GetAxis("Vertical") > 0) {
                speedInput = Input.GetAxis("Vertical") * forwardAccel * 500f;
            } 
            else if (Input.GetAxis("Vertical") < 0) {
                speedInput = Input.GetAxis("Vertical") * reverseAccel * 500f;
            } 
            turnInput = Input.GetAxis("Horizontal");
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3 (0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));

            transform.position = sphere.transform.position;
            drifting = true;
        }
        if (drifting) {
            timer += Time.deltaTime;
            if (timer >= 2) {
                Debug.Log("Drifting complete");
            }
        }
        if (isSpeedBoosted) {
            speedTimer += Time.deltaTime;
            forwardAccel = 32f;
            maxSpeed = 160f;
            if (speedTimer >= 1)
            {
                forwardAccel = 8f;
                maxSpeed = 50f;
                isSpeedBoosted = false;
                speedTimer = 0;
            }
        }
    }

    private void FixedUpdate() {
        if (Mathf.Abs(speedInput) > 0) {
            sphere.AddForce(transform.forward * speedInput);
        }
    }
}
