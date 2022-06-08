using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public delegate void OnMinigameDone(int gameIndex);
public delegate void OnRunningMinigame(int gameIndex);
public class MinigameManager : MonoBehaviour
{

    public GameObject maze;
    public GameObject wheel;
    public GameObject line;
    public GameObject touchpad;

    public GameObject environmentMachine;
    public GameObject humidMachine;
    public GameObject tempatureMachine;

    public GameObject product;
    public OnMinigameDone onMinigameDone;
    public OnRunningMinigame onRunningMinigame;
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        onRunningMinigame += RunGame;
        onMinigameDone += Endgame;
    }


    void RunGame(int gameIndex)
    {
        switch (gameIndex)
        {
            case 0:
                touchpad.SetActive(false);
                if (maze != null)
                    maze.SetActive(true);
                break;
            case 1:
                touchpad.SetActive(false);
                if (wheel != null)
                    wheel.SetActive(true);
                break;
            case 2:
                touchpad.SetActive(false);
                if (line != null)
                    line.SetActive(true);
                break;
        }
    }

    void Endgame(int gameIndex)
    {
        switch (gameIndex)
        {
            case 0:
                Instantiate(product, environmentMachine.transform.position - new Vector3(0, 0, 2), new Quaternion(0,0,0,0));
                touchpad.SetActive(true);
                maze.SetActive(false);
                break;
            case 1:
                Instantiate(product, humidMachine.transform.position - new Vector3(0, 0, 2), new Quaternion(0, 0, 0, 0));
                touchpad.SetActive(true);
                wheel.SetActive(false);
                break;
            case 2:
                Instantiate(product, tempatureMachine.transform.position - new Vector3(0, 0, 2), new Quaternion(0, 0, 0, 0));
                touchpad.SetActive(true);

                line.SetActive(false);
                break;

        }
    }

    public void Minigame(int i)
    {
        onRunningMinigame.Invoke(i);
    }
    public void EndGame(int i)
    {
        onMinigameDone.Invoke(i);
    }

    public void resetGame()
    {
        SceneManager.LoadScene(0);
    }
}
