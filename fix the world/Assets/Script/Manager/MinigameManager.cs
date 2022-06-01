using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnMinigameDone(int gameIndex);
public delegate void OnRunningMinigame(int gameIndex);
public class MinigameManager : MonoBehaviour
{
    public GameObject maze;
    public GameObject wheel;
    public GameObject line;
    public GameObject touchpad;

    public OnMinigameDone onMinigameDone;
    public OnRunningMinigame onRunningMinigame;

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
                touchpad.SetActive(true);
                maze.SetActive(false);
                break;
            case 1:
                touchpad.SetActive(true);
                wheel.SetActive(false);
                break;
            case 2:
                touchpad.SetActive(true);

                line.SetActive(false);
                break;

        }
    }
}
