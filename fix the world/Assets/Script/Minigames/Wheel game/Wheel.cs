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

        if (Input.GetMouseButton(0)||Input.touchCount>0)
        {
            if (Input.touchCount > 0)
            {
                dif = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10)) - transform.position;
            }
            else
            {
                dif = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)) - transform.position;
            }
            dif.Normalize();
            float rotateZ = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
            Debug.Log(rotateZ);
            transform.localRotation = Quaternion.AngleAxis(rotateZ,Vector3.forward);
            progress += 0.025f;

            //restric the rotating angle
            /* float degreeAngle;
             if (transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 180)
             {
                 if (rotateZ < transform.eulerAngles.z)
                 {
                     if (rotateZ > -10)
                     {
                         progress += 0.035f;
                         transform.LeanRotate(new Vector3(0, 0, rotateZ), 0.1f);
                     }

                 }

             }
             if (transform.eulerAngles.z > 180 && transform.eulerAngles.z < 360)
             {
                 degreeAngle = 360 - transform.localEulerAngles.z;
                 if (rotateZ >= 0 && rotateZ < 180)
                 {
                     if (rotateZ >= -degreeAngle)
                     {
                         if (rotateZ > 170)
                         {
                             progress += 0.035f;
                             transform.LeanRotate(new Vector3(0, 0, rotateZ), 0.1f);
                         }

                     }
                 }
                 else
                 if (rotateZ < 0 && rotateZ >= -180)
                     if (rotateZ < -degreeAngle)
                     {
                         progress += 0.035f;
                         transform.LeanRotate(new Vector3(0, 0, rotateZ), 0.1f);
                     }
             }
 */



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
