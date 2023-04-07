using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DailySchedule : MonoBehaviour
{

    public int currentHour = 0;

    public float currentTime = 0;

    public HeartRate heartManager;

    public GameObject activityDisplay;

    // Start is called before the first frame update
    void Start()
    {
        activityDisplay.transform.GetChild(2).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime + Time.deltaTime;
        if (currentTime > 30) {
            currentHour++;
            if (currentHour > 9) {
                currentHour = 0;
            }
            currentTime = 0;
            updateActivity();
        }
    }
    
    void updateActivity() {
        for (int i = 0; i < 4; i++) {
            activityDisplay.transform.GetChild(i).gameObject.SetActive(false);
        }
        if (currentHour == 1 || currentHour == 4 || currentHour == 8 ) {
            heartManager.startEating();
            activityDisplay.transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (currentHour == 0 || currentHour == 5 || currentHour == 9 ) {
            heartManager.startRest();
            activityDisplay.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (currentHour == 2 || currentHour == 7) {
            heartManager.startExercise();
            activityDisplay.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (currentHour == 3 || currentHour == 6 ) {
            heartManager.startWorking();
            activityDisplay.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
