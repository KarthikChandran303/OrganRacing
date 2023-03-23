using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DailySchedule : MonoBehaviour
{

    public int currentHour = 0;

    public float currentTime = 0;

    public HeartRate heartManager;

    [SerializeField] TMP_Text UILabel;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (currentHour == 1 || currentHour == 4 || currentHour == 8 ) {
            heartManager.startEating();
            UILabel.text = " Thomas Activity: Eating";
        }
        if (currentHour == 0 || currentHour == 5 || currentHour == 9 ) {
            heartManager.startRest();
            UILabel.text = " Thomas Activity: Resting";
        }
        if (currentHour == 2 || currentHour == 7) {
            heartManager.startExercise();
            UILabel.text = " Thomas Activity: Exercising";
        }
        if (currentHour == 3 || currentHour == 6 ) {
            heartManager.startWorking();
            UILabel.text = " Thomas Activity: Working";
        }
    }
}
