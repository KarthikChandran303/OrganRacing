using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{

    public Rigidbody sphere;
    public float forwardAccel = 8f;
    public float reverseAccel = 4f;
    public float turnStrength = 60f;
    public float driftStrength = 130f;
    public float timer = 0;
    public float speedTimer = 0;
    public float driftTime = 0;
    public bool drifting = false;
    public bool isSpeedBoosted = false;
    public float boostTime = 0;
    private float speedInput;
    private float turnInput;
    private float driftDirection;


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
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        } 
        else if (Input.GetAxis("Vertical") < 0) {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        } 
        turnInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("space") && !drifting && turnInput != 0) {
            drifting = true;
            driftDirection = turnInput;
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
            speedBoost();
        }

        if (isSpeedBoosted) {
            if (boostTime > 0) {
                boostTime = boostTime - Time.deltaTime;
            }
            else {
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
        if (Mathf.Abs(speedInput) > 0) {
            if (isSpeedBoosted) {
                sphere.AddForce(transform.forward * 2 * speedInput);
            }
            else {
                sphere.AddForce(transform.forward * speedInput);
            }
        }
    }

    public void onCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Booster") {
            isSpeedBoosted = true;
            boostTime = 3;
        }
    }
}
