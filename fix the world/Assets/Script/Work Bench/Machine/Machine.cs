using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System;

public enum MachineTypes
{
    environment,
    humid,
    tempature
}

public class Machine : MonoBehaviour
{
    public Image alertUI;
    Machine currentuse;
    public TaskManager tasksManager;
    public MachineTypes machineType;
    public MinigameManager minigameManager;
    bool Done;

    GameObject plantUsed;
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }*/
    }
    private void Start()
    {
        Done = false;
        minigameManager.onMinigameDone += DestroyPlantUse;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!Done)
           
            if (other.gameObject.tag == "Plant")
            {
                if (Input.GetMouseButtonUp(0))
                    switch (machineType)
                {
                    case MachineTypes.environment:
                        if (other.gameObject.GetComponent<Augment>() != null)
                            if (other.gameObject.GetComponent<Augment>().environment == tasksManager.trop)
                            {
                                Debug.Log("1");
                                currentuse = this;
                                plantUsed = other.gameObject;
                                minigameManager.onRunningMinigame?.Invoke(0);
                            }
                            else
                            {
                                other.GetComponent<dragObject>().RemoveObj();
                                //false part animation
                            }
                        break;
                    case MachineTypes.humid:
                        if (other.gameObject.GetComponent<Augment>() != null)
                            if (other.gameObject.GetComponent<Augment>().humid == tasksManager.humid)
                            {
                                Debug.Log("2");
                                plantUsed = other.gameObject;
                                currentuse = this;
                                minigameManager.onRunningMinigame?.Invoke(1);
                            }
                            else
                            {
                                other.GetComponent<dragObject>().RemoveObj();
                                //false part animation
                            }
                        break;
                    case MachineTypes.tempature:
                        if (other.gameObject.GetComponent<Augment>() != null)
                            if (other.gameObject.GetComponent<Augment>().temp == tasksManager.temp)
                            {
                                Debug.Log("3");
                                currentuse = this;
                                plantUsed = other.gameObject;
                                minigameManager.onRunningMinigame?.Invoke(2);
                            }
                            else
                            {
                                other.GetComponent<dragObject>().RemoveObj();
                                //false part animation
                            }
                        break;
                }
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    void DestroyPlantUse(int i)
    {
        if(currentuse != null)
        {
            currentuse.alertUI.color = Color.green;
            currentuse.Done = true;
        }
        if(plantUsed != null)
        Destroy(plantUsed);
    }
}
