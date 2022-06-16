using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Wheel : MonoBehaviour
{
    public MinigameManager minigameManager;
    public float winRate = 20;
    public float progress;
    public Slider progressbar;
    public Gradient grad;
    public Image fill;
    public Camera cam;
    Vector3 dif;
    private void Start()
    {
        progressbar.maxValue = winRate;
        fill.color = grad.Evaluate(0f);
    }

    private void Update()
    {
        if (progress >= 0)
            progress -= 0.005f;
        //updating bar
        progressbar.value = progress;
        fill.color = grad.Evaluate(progressbar.normalizedValue);

        if (Input.touchCount>0)
        {
           

            dif = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10)) - transform.position;
         
            dif.Normalize();
            float rotateZ = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
            Debug.Log(rotateZ);
            transform.localRotation = Quaternion.AngleAxis(rotateZ,Vector3.forward);
            progress += 0.025f;

        }

        Win();
    }

    //if win
    void Win()
    {
        if (progress >= winRate)
        {
            minigameManager.onMinigameDone?.Invoke(1);
        }
    }

}
