using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Drive : MonoBehaviour
{
    public GameObject minimapIcon;

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
    private bool stationaryTurning;
    public HeartRate heartManager;
    [SerializeField] TMP_Text speedometer;

    [Header("Sounds")]
    [SerializeField] AudioSource engineSource;
    [SerializeField] AudioClip engineRev;
    [SerializeField] AudioClip engineOn;

    [SerializeField] AudioSource driftClickSource;
    [SerializeField] AudioClip driftClick1;
    [SerializeField] AudioClip driftClick2;
    [SerializeField] AudioClip driftClick3;

    [SerializeField] AudioSource driftBoostSource;
    [SerializeField] AudioClip driftBoost1;
    [SerializeField] AudioClip driftBoost2;
    [SerializeField] AudioClip driftBoost3;

    [SerializeField] AudioSource wheelsSound;
    [SerializeField] AudioSource impactSound;

    [Header("Animation")]
    [SerializeField] Animator bikeAnim;
    [SerializeField] Animator racerAnim;

    // Start is called before the first frame update
    void Start()
    {
       sphere.transform.parent = null;
       Instantiate(minimapIcon, transform);
    }

    // Update is called once per frame
    void Update()
    {
        float heartSpeed = heartManager.getCurrentRate() / 100;

        if (speedometer != null)
            speedometer.text = ((int) sphere.velocity.magnitude).ToString();

        speedInput = 0f;

        float dir = Input.GetAxis("Forward") - Input.GetAxis("Backward");

        //if (Input.GetAxis("Forward") > 0 && Input.GetAxis("Backward") > 0) {
        //    stationaryTurning = true;
        //}
        //else if...
        if (dir > 0) {
            speedInput = dir * forwardAccel * 1500f;
            //stationaryTurning = false;
        } 
        else if (dir < 0) {
            speedInput = dir * reverseAccel * 1500f;
            drifting = false;
            //stationaryTurning = false;
        }

        turnInput = Input.GetAxis("Horizontal");

        //Pass Animation Values
        bikeAnim.SetFloat("BlendX", turnInput);
        bikeAnim.SetFloat("BlendY", dir);
        racerAnim.SetFloat("TurnBlend", turnInput);

        if (Input.GetAxis("Drift") > 0 && turnInput != 0) {
            driftCheck = true;
        }
        else {
            driftCheck = false;
        }

        if (driftCheck && !drifting && turnInput != 0) {
            drifting = true;
            if (turnInput > 0) {
                driftDirection = 1;
            } else {
                driftDirection = -1;
            }
            driftCheck = false;
        } 
        else { //Normal Turning
            //Calculate Angular Drag based on current velocity
            float angularDrag = Mathf.Min(10 / (sphere.GetPointVelocity(sphere.position).magnitude + 1), .7f);
            Debug.Log(angularDrag);


            if (dir > 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + transform.up * (turnInput * turnStrength * Time.deltaTime * 2f * angularDrag));
            }
            else if (dir == 0 && Input.GetAxis("Forward") > 0 && Input.GetAxis("Backward") > 0) //Turn slowly when using both forward and reverse input, don't apply angular drag
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + transform.up * (turnInput * turnStrength * Time.deltaTime * 1f));
            }
            else if (dir < 0) //Turn quickly and opposite direction if backing up, don't apply angular drag
            { 
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + transform.up * (turnInput * turnStrength * Time.deltaTime * -2f));
            }
            //else do nothing
            
            transform.position = sphere.transform.position;
        }

        //Stationary Turning
        //if (stationaryTurning && turnInput != 0) {
        //    transform.Rotate(0, 2f * Time.deltaTime * turnInput * turnStrength, 0, Space.Self);
        //    transform.position = sphere.transform.position;
        //}

        if (drifting) {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + transform.up * (driftDirection * driftStrength * Time.deltaTime * .5f));
            transform.position = sphere.transform.position;
            driftTime = driftTime + Time.deltaTime;
            if (driftTime > 3) {
                Debug.Log("Drift level 3");
                if (driftClickSource.clip != driftClick3)
                {
                    driftClickSource.clip = driftClick3;
                    driftClickSource.Play();
                }
            }
            else if (driftTime > 2) {
                Debug.Log("Drift level 2");
                if(driftClickSource.clip != driftClick2)
                {
                    driftClickSource.clip = driftClick2;
                    driftClickSource.Play();
                }
            }
            else if (driftTime > 1) {
                Debug.Log("Drift level 1");
                if (driftClickSource.clip != driftClick1)
                {
                    driftClickSource.clip = driftClick1;
                    driftClickSource.Play();
                }
            }
        }

        if (Input.GetAxis("Drift") == 0 && drifting) {
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
            driftBoostSource.clip = driftBoost3;
            driftBoostSource.Play();
        }
        else if (driftTime > 2) {
            isSpeedBoosted = true;
            boostTime = 2;
            driftBoostSource.clip = driftBoost2;
            driftBoostSource.Play();
        }
        else if (driftTime > 1) {
            isSpeedBoosted = true;
            boostTime = 1;
            driftBoostSource.clip = driftBoost1;
            driftBoostSource.Play();
        }
        driftTime = 0;
        driftClickSource.clip = null;
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

        float speedMag = sphere.velocity.sqrMagnitude;
        if (speedMag < 1000)
        {
            wheelsSound.volume = speedMag / 1000;
        }
        else
        {
            wheelsSound.volume = speedMag;
        }
    }

    public void BounceImpact()
    {
        Vector3 vel = sphere.velocity;
        sphere.AddForce(-vel * 10000);

        PlayImpactSound();
    }

    private void PlayImpactSound()
    {
        float speedMag = sphere.velocity.sqrMagnitude;
        if (speedMag < 1000)
        {
            impactSound.volume = speedMag / 1000;
        }
        else
        {
            impactSound.volume = speedMag;
        }

        impactSound.Play();
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
