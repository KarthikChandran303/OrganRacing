using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
public class Drive : MonoBehaviour
{
    public GameObject minimapIcon;
    public GameObject meshBody; 

    public Rigidbody sphere;
    public float forwardAccel = 8f;
    public float reverseAccel = 4f;
    public float turnStrength = 60f;
    public float driftStrength = 120f;
    public float turnAngle = 50f;
    public float timer = 0;
    public float speedTimer = 0;
    public float turnTimer = 0;
    public float maxTurnTime = 1.5f;
    public float driftTime = 0;
    public bool drifting = false;
    public bool isDriftBoosted = false;
    public bool isSpeedBoosted = false;
    public bool isSpeedReduced = false;
    public bool driftCheck = false;
    public float boostTime = 0;
    private float speedInput;
    private float turnInput;
    private float driftDirection;
    //private bool stationaryTurning;
    public HeartRate heartManager;
    [SerializeField] TMP_Text speedometer;

    GameObject minimapIconObj;

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

    [SerializeField] AudioSource wheelsSource;
    [SerializeField] AudioClip wheelsNormal;
    [SerializeField] AudioClip wheelsDrift1;
    [SerializeField] AudioClip wheelsDrift2;
    [SerializeField] AudioClip wheelsDrift3;

    [SerializeField] AudioSource impactSound;

    [Header("Animation")]
    [SerializeField] Animator bikeAnim;
    [SerializeField] Animator racerAnim;

    [Header("Effects")]
    [SerializeField] ParticleSystem driftSparks_1A;
    [SerializeField] ParticleSystem driftSparks_2A;
    [SerializeField] ParticleSystem driftSparks_3A;
    [SerializeField] ParticleSystem driftSparks_1B;
    [SerializeField] ParticleSystem driftSparks_2B;
    [SerializeField] ParticleSystem driftSparks_3B;
    [SerializeField] ParticleSystem boostFX;
    [SerializeField] ParticleSystem dustFX;
    [SerializeField] ParticleSystem highSpeedLines;
    [SerializeField] ParticleSystem lowSpeedLines;


    // Start is called before the first frame update
    void Start()
    {
       sphere.transform.parent = null;
       minimapIconObj = Instantiate(minimapIcon);
    }

    // Update is called once per frame
    void Update()
    {
        minimapIconObj.transform.position = new Vector3(transform.position.x, minimapIconObj.transform.position.y, transform.position.z);

        float heartSpeed = heartManager.getCurrentRate() / 100;

        if (speedometer != null)
            speedometer.text = ((int) sphere.velocity.magnitude).ToString();

        //if speed is above ~35~ then activate
        if (sphere.velocity.magnitude > 35f) 
        {
            lowSpeedLines.Play();
        }
        else {
            lowSpeedLines.Stop();
        }

        speedInput = 0f;

        float dir = Input.GetAxis("Forward") - Input.GetAxis("Backward");

        //if (Input.GetAxis("Forward") > 0 && Input.GetAxis("Backward") > 0) {
        //    stationaryTurning = true;
        //}
        //else if...
        if (dir > 0)
        {
            speedInput = dir * forwardAccel * 1500f;
            //stationaryTurning = false;
            dustFX.Play();

        }
        else if (dir < 0)
        {
            speedInput = dir * reverseAccel * 1500f;
            drifting = false;
            //stationaryTurning = false;
            dustFX.Play();
        }
        else if (Input.GetAxis("Forward") == 0 && Input.GetAxis("Backward") == 0)
        {
            dustFX.Stop();
        }

        turnInput = Input.GetAxis("Horizontal");

        
        //Pass Animation Values
        bikeAnim.SetFloat("BlendX", turnInput);
        bikeAnim.SetFloat("BlendY", dir);
        racerAnim.SetFloat("BlendX", turnInput);
        racerAnim.SetFloat("BlendY", dir);

        if (Input.GetButtonDown("Drift")) {
            sphere.AddForce(transform.up * 20000f);
        }

        if (Input.GetButton("Drift") && turnInput != 0) { //Should it be 'Input.GetAxis("Drift") > 0' or what's currently there?
            driftCheck = true;
        }
        else {
            driftCheck = false;
        }

        //Calculate Angular Drag for bike based on current velocity
        //float angularDrag = 1 + Mathf.Log((sphere.velocity.magnitude) + 1 , 500f);


        if (driftCheck && !drifting && turnInput != 0) {
            drifting = true;
            if (turnInput > 0) {
                driftDirection = 1;
            } else {
                driftDirection = -1;
            }
            driftCheck = false;
        } 
        else {

            // Normal Turning //

            if (dir > 0) //
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + transform.up * (turnInput * turnStrength * Time.deltaTime)); // / angularDrag));
            }
            else if (dir == 0 && Input.GetAxis("Forward") > 0 && Input.GetAxis("Backward") > 0) //Turn slowly when using both forward and reverse input
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + transform.up * (turnInput * turnStrength * Time.deltaTime)); // / angularDrag));
            }
            else if (dir < 0) //Turn quickly and opposite direction if backing up
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + transform.up * (turnInput * turnStrength * Time.deltaTime * -1f)); // / angularDrag));
            }
            //else do nothing
            
            transform.position = sphere.transform.position;
        }

        // Turn Timer & Tilting //
        float rotStep = 20f * Time.deltaTime;
        if (turnInput != 0 && !drifting) //&& turnTimer < maxTurnTime)
        {
            //turnTimer += Time.deltaTime;
            if (turnInput > 0) //&& isBike
            {
                meshBody.transform.localRotation = Quaternion.RotateTowards(meshBody.transform.localRotation, Quaternion.Euler(0, 0, -turnAngle), 10f * turnInput * rotStep);
            }
            else if (turnInput < 0) //&& isBike
            {
                meshBody.transform.localRotation = Quaternion.RotateTowards(meshBody.transform.localRotation, Quaternion.Euler(0, 0, turnAngle), 10f * -turnInput * rotStep);
            }
        }
        else if (turnInput == 0 && !drifting) //Tilt bike when not turning to be upright
        {
            //turnTimer = 0;
            meshBody.transform.localRotation = Quaternion.RotateTowards(meshBody.transform.localRotation, Quaternion.Euler(0, 0, 0), rotStep);
        }

        // Stationary Turning // **DEPRECATED**
        //if (stationaryTurning && turnInput != 0) {
        //    transform.Rotate(0, 2f * Time.deltaTime * turnInput * turnStrength, 0, Space.Self);
        //    transform.position = sphere.transform.position;
        //}

        // Wheelie code //
        if (isSpeedBoosted) {
            meshBody.transform.localRotation = Quaternion.RotateTowards(meshBody.transform.localRotation, Quaternion.Euler(-30, 0, 0), 4f * rotStep);
        }
        else {
            meshBody.transform.localRotation = Quaternion.RotateTowards(meshBody.transform.localRotation, Quaternion.Euler(0, 0, 0), 4f * rotStep);
        }

        // Drifting Code //
        if (drifting)
        {
            //Drift Leaning
            if (driftDirection > 0)
            {
                meshBody.transform.localRotation = Quaternion.RotateTowards(meshBody.transform.localRotation, Quaternion.Euler(0, 0, -turnAngle), 10f * rotStep);
            }
            else if (driftDirection < 0)
            {
                meshBody.transform.localRotation = Quaternion.RotateTowards(meshBody.transform.localRotation, Quaternion.Euler(0, 0, turnAngle), 10f * rotStep);
            }


            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + transform.up * (driftDirection * driftStrength * Time.deltaTime)); // / angularDrag));
            transform.position = sphere.transform.position;
            driftTime = driftTime + Time.deltaTime;
            if (driftTime > 3f)
            {
                if (!driftSparks_3B.isPlaying)
                {
                    driftSparks_2B.Stop();
                    driftSparks_3A.Play();
                    driftSparks_3B.Play();
                }
                if (driftClickSource.clip != driftClick3)
                {
                    driftClickSource.clip = driftClick3;
                    driftClickSource.Play();
                    wheelsSource.clip = wheelsDrift3;
                    wheelsSource.Play();
                }
            }
            else if (driftTime > 1.5f)
            {
                //Update Effects
                if (!driftSparks_2B.isPlaying)
                {
                    driftSparks_1B.Stop();
                    driftSparks_2A.Play();
                    driftSparks_2B.Play();
                }
                if (driftClickSource.clip != driftClick2)
                {
                    driftClickSource.clip = driftClick2;
                    driftClickSource.Play();
                    wheelsSource.clip = wheelsDrift2;
                    wheelsSource.Play();
                }
            }
            else if (driftTime > .75f)
            {

                //Update Effects
                if (!driftSparks_1B.isPlaying)
                {
                    driftSparks_1A.Play();
                    driftSparks_1B.Play();
                }
                if (driftClickSource.clip != driftClick1)
                {
                    driftClickSource.clip = driftClick1;
                    driftClickSource.Play();
                    wheelsSource.clip = wheelsDrift1;
                    wheelsSource.Play();
                }
            }
        }
        else {
            //Stop Drift Effects when not drifting
            if (driftSparks_1B.isPlaying)
            {
                driftSparks_1B.Stop();
            }
            if (driftSparks_2B.isPlaying)
            {
                driftSparks_2B.Stop();
            }
            if (driftSparks_3B.isPlaying)
            {
                driftSparks_3B.Stop();
            }
        }

        if (Input.GetAxis("Drift") == 0 && drifting) {
            driftCheck = false;
            if (drifting) DriftBoost();
        }

        if (isDriftBoosted || isSpeedReduced || isSpeedBoosted) {
            if (boostTime > 0) {
                boostTime = boostTime - Time.deltaTime;
            }
            else {
                isSpeedReduced = false;
                isDriftBoosted = false;
                isSpeedBoosted = false;
            }
        }
    }

    public void DriftBoost() {
        drifting = false;
        

        //Check boost strength
        if (driftTime > 3) {
            isDriftBoosted = true;
            boostTime = 3;
            driftBoostSource.clip = driftBoost3;
            driftBoostSource.Play();
            boostFX.Play();
        }
        else if (driftTime > 1.5) {
            isDriftBoosted = true;
            boostTime = 2;
            driftBoostSource.clip = driftBoost2;
            driftBoostSource.Play();
            boostFX.Play();
        }
        else if (driftTime > .75) {
            isDriftBoosted = true;
            boostTime = 1;
            driftBoostSource.clip = driftBoost1;
            driftBoostSource.Play();
            boostFX.Play();
        }
        driftTime = 0;
        wheelsSource.clip = wheelsNormal;
        wheelsSource.Play();
        driftClickSource.clip = null;

        //Play Boost Speed Lines
        if (isDriftBoosted)
            highSpeedLines.Play();
    }

    private void FixedUpdate() {
        float heartSpeed = heartManager.getCurrentRate() / 100;
        if (Mathf.Abs(speedInput) > 0) {
            if (isDriftBoosted || isSpeedBoosted) {
                sphere.AddForce(transform.forward * 1.5f * speedInput * heartSpeed * Time.deltaTime * 30f);
            }
            else if (isSpeedReduced) {
                sphere.AddForce(transform.forward * 0.5f * speedInput * heartSpeed  * Time.deltaTime * 30f);
            }
            else {
                sphere.AddForce(transform.forward * speedInput * heartSpeed  * Time.deltaTime * 30f);
            }
        }

        float speedMag = sphere.velocity.sqrMagnitude;
        if (speedMag < 1000 && driftTime < 0.6f)
        {
            wheelsSource.volume = speedMag / 1000;
        }
        else
        {
            wheelsSource.volume = 1;
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
        isDriftBoosted = false;
        isSpeedReduced = false;
        boostTime = 5;
    }

    public void ApplySpeedReduction()
    {
        isSpeedBoosted = false;
        isSpeedReduced = true;
        isDriftBoosted = false;
        boostTime = 3;
    }
}
