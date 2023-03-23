using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{

    public Rigidbody sphere;
    public float forwardAccel = 8f;
    public float reverseAccel = 4f;
    public float turnStrength = 60f;
    public float driftStrength = 120f;
    public float timer = 0;
    public float speedTimer = 0;
    public float driftTime = 0;
    public bool drifting = false;
    public bool isSpeedBoosted = false;
    public bool isSpeedReduced = false;

    public bool driftCheck = false;
    public float boostTime = 0;
    private float speedInput;
    private float turnInput;
    private float driftDirection;
    public HeartRate heartManager;

    // Start is called before the first frame update
    void Start()
    {
       sphere.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0f;
        if (Input.GetAxis("Vertical") > 0) {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1500f;
        } 
        else if (Input.GetAxis("Vertical") < 0) {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1500f;
            drifting = false;
        } 
        turnInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("space")) {
            driftCheck = true;
        }

        if (driftCheck && !drifting && turnInput != 0) {
            drifting = true;
            driftDirection = turnInput;
            driftCheck = false;
        } 
        else {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3 (0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
            transform.position = sphere.transform.position;
        }

        if (drifting) {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3 (0f, driftDirection * driftStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
            transform.position = sphere.transform.position;
            driftTime = driftTime + Time.deltaTime;
            if (driftTime > 3) {
                Debug.Log("Drift level 3");
            }
            else if (driftTime > 2) {
                Debug.Log("Drift level 2");
            }
            else if (driftTime > 1) {
                Debug.Log("Drift level 1");
            }
        }

        if (Input.GetKeyUp("space") && drifting) {
            driftCheck = false;
            if (drifting) speedBoost();
        }

        if (isSpeedBoosted || isSpeedReduced) {
            if (boostTime > 0) {
                boostTime = boostTime - Time.deltaTime;
            }
            else {
                isSpeedReduced = false;
                isSpeedBoosted = false;
            }
        }
    }

    public void speedBoost() {
        drifting = false;
        if (driftTime > 3) {
            isSpeedBoosted = true;
            boostTime = 3;
        }
        else if (driftTime > 2) {
            isSpeedBoosted = true;
            boostTime = 2;
        }
        else if (driftTime > 1) {
            isSpeedBoosted = true;
            boostTime = 1;
        }
        driftTime = 0;
    }

    private void FixedUpdate() {
        float heartSpeed = heartManager.getCurrentRate() / 100;
        if (Mathf.Abs(speedInput) > 0) {
            if (isSpeedBoosted) {
                sphere.AddForce(transform.forward * 1.5f * speedInput * heartSpeed);
            }
            else if (isSpeedReduced) {
                sphere.AddForce(transform.forward * 0.5f * speedInput * heartSpeed);
            }
            else {
                sphere.AddForce(transform.forward * speedInput * heartSpeed);
            }
        }
    }

    public void OnCollisionEnter(Collision col) {
/*        Debug.Log("hello");
        if (col.gameObject.tag == "Booster") {

        }*/
    }

    public void ApplySpeedBoost()
    {
        isSpeedBoosted = true;
        isSpeedReduced = false;
        boostTime = 3;
    }

    public void ApplySpeedReduction()
    {
        isSpeedReduced = true;
        isSpeedBoosted = false;
        boostTime = 3;
    }
}
