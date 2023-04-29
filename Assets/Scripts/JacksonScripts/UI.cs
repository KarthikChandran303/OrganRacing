using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{

    bool isPaused;

    public GameObject main;

    public GameObject pause;

    public GameObject gameOverScreen;

    public TMP_Text deathMessage;

    bool lost;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        lost = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && !lost) {
            if (!isPaused) {
                pauseGame();
            }
            else if (isPaused) {
                resumeGame();
            }         
        }
    }

    public void startGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("OrganMap");
        lost = false;

    }

    public void startGameAfterDeath() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
        SceneManager.LoadScene("OrganMap");
        lost = false;

    }

    public void toTitle() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    public void pauseGame() {
        Time.timeScale = 0;
        main.SetActive(false);
        pause.SetActive(true);
        isPaused = true;
    }

    public void gameOver() {
        Time.timeScale = 0;
        main.SetActive(false);
        pause.SetActive(false);
        lost = true;
        double survivalTime = Time.timeSinceLevelLoadAsDouble;
        double survivalMins = survivalTime / 60;
        int mins = (int) survivalMins;
        double survivalSecs = survivalTime % 60;
        int secs = (int) survivalSecs;
        deathMessage.text = "You survived for " + mins + " minutes and " + secs + " seconds!";
        gameOverScreen.SetActive(true);
    }

    public void resumeGame() {
        Time.timeScale = 1;
        main.SetActive(true);
        pause.SetActive(false);
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
