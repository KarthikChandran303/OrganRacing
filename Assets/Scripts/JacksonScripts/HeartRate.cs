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

    public float heartDeteriorationFactor = 1;

    public float bloodCellEffectiveness = 10f;

    [SerializeField] TMP_Text UILabel;

    public int organsInDanger = 0;
    public int organsDying = 0;

    [Header("Music")]
    [SerializeField] AudioClip fullHealthMusic;
    [SerializeField] AudioClip dangerMusic;
    [SerializeField] AudioClip dyingMusic;

    [SerializeField] AudioSource music;

    public void ChangeMusic()
    {
        if (organsDying > 0)
        {
            PlayClip(dyingMusic);
        }
        else if (organsInDanger > 0)
        {
            PlayClip(dangerMusic);
        }
        else
        {
            PlayClip(fullHealthMusic);
        }
    }

    void PlayClip(AudioClip clip)
    {
        if (music.clip != clip)
        {
            music.clip = clip;
            music.Play();
        }
    }

    public void OrganStatusChange(int dDanger, int dDying)
    {
        organsInDanger += dDanger;
        organsDying += dDying;
        ChangeMusic();
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
