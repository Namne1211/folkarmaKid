using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public delegate void OnMinigameDone(int gameIndex);
public delegate void OnRunningMinigame(int gameIndex);
public class MinigameManager : MonoBehaviour
{
    public TaskManager taskManager;
    [Space]
    public List<GameObject> environmentalProduct;
    public List<GameObject> humidProduct;
   
    public List<GameObject> tempatureProduct;
    [Space]
    public GameObject maze;
    public GameObject wheel;
    public GameObject line;
    public GameObject touchpad;
    [Space]
    public GameObject environmentMachine;
    public GameObject humidMachine;
    public GameObject tempatureMachine;
    [Space]
    public GameObject enProduct;
    public GameObject huProduct;
    public GameObject teProduct;
    public OnMinigameDone onMinigameDone;
    public OnRunningMinigame onRunningMinigame;
    [Space]
    public GameObject cylinderPlace;
    public Transform spawnplace;
    public Transform glass;

    public UnityEvent Startgame;
    public UnityEvent WinEvent;
    public UnityEvent StartBasemachineEvent;
    public UnityEvent StartHumidmachineEvent;
    public UnityEvent StartTempmachineEvent;
    public UnityEvent StopMachineEvent;





    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        Startgame.Invoke();

    }
    private void Start()
    {
        onRunningMinigame += RunGame;
        onMinigameDone += Endgame;

        switch (taskManager.trop)
        {
            case (Environment.artic):
                enProduct = environmentalProduct[0];
                break;
            case (Environment.tropical):
                enProduct = environmentalProduct[1];
                break;
            case (Environment.desert):
                enProduct = environmentalProduct[2];
                break;
        }
        switch (taskManager.humid)
        {
            case (humid.moist):
                huProduct = humidProduct[0];
                break;
            case (humid.wet):
                huProduct =
                huProduct = humidProduct[1];
                break;
            case (humid.dry):
                huProduct =
                huProduct = humidProduct[2];
                break;
        }
        switch (taskManager.temp)
        {
            case (tempature.cold):
                teProduct = tempatureProduct[0];
                break;
            case (tempature.warm):
                teProduct = tempatureProduct[1];
                break;
            case (tempature.hot):
                teProduct = tempatureProduct[2];
                break;
        }
    }


    void RunGame(int gameIndex)
    {
        switch (gameIndex)
        {
            case 0:
                touchpad.SetActive(false);
                if (maze != null)
                    maze.SetActive(true);
                StartBasemachineEvent.Invoke();

                break;
            case 1:
                touchpad.SetActive(false);
                if (wheel != null)
                    wheel.SetActive(true);
                StartHumidmachineEvent.Invoke();
                break;
            case 2:
                touchpad.SetActive(false);
                if (line != null)
                    line.SetActive(true);
                StartTempmachineEvent.Invoke();
                break;
        }
    }

    void Endgame(int gameIndex)
    {
        switch (gameIndex)
        {
            case 0:
                Instantiate(huProduct, humidMachine.transform.position - new Vector3(0, 0, 1.5f), new Quaternion(0, 0, 0, 0));
               
                touchpad.SetActive(true);
                StopMachineEvent.Invoke();
                Destroy(maze);
                break;
            case 1:
                Instantiate(enProduct, spawnplace.position, spawnplace.rotation);
                if (cylinderPlace != null)
                    Destroy(cylinderPlace.gameObject);
                glass.LeanRotate(new Vector3(60, 0, 0), 0.5f);
                touchpad.SetActive(true);
                StopMachineEvent.Invoke();
                Destroy(wheel);
                break;
            case 2:
                Instantiate(teProduct, tempatureMachine.transform.position - new Vector3(0, 0.5f, 1.3f), new Quaternion(0, 0, 0, 0));
                touchpad.SetActive(true);

                StopMachineEvent.Invoke();
                Destroy(line);
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
        WinEvent.Invoke();
    }

    public void resetGame()
    {
        Combiner.playtime += 1;
        PlayerPrefs.SetInt("playTime", Combiner.playtime);
        SceneManager.LoadScene(0);
        
    }
}
