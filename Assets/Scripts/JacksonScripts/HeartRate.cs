using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeartRate : MonoBehaviour
{

    float currentHeartRate = 60;

    float restingHeartRate = 60;

    float exercisingHeartRate = 120;

    float workingHeartRate = 80;

    float eatingHeartRate = 100;

    [SerializeField] TMP_Text UILabel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UILabel.text = " Heart rate: " + (int) currentHeartRate;
    }

    public void startRest() {
        currentHeartRate = restingHeartRate;
    }

    public void startExercise() {
        currentHeartRate = exercisingHeartRate;
    }

    public void startEating() {
        currentHeartRate = eatingHeartRate;
    }

    public void startWorking() {
        currentHeartRate = workingHeartRate;
    }

    public float getCurrentRate() {
        return currentHeartRate;
    }
}
