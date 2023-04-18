using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{

    bool isPaused;

    public GameObject main;

    public GameObject pause;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused) {
                pauseGame();
            }
            else if (isPaused) {
                resumeGame();
            }         
        }
    }

    public void startGame() {
         SceneManager.LoadScene("OrganMap");
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

    public void resumeGame() {
        Time.timeScale = 1;
        main.SetActive(true);
        pause.SetActive(false);
        isPaused = false;
    }

}
