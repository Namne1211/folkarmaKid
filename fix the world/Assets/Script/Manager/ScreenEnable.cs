using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEnable : MonoBehaviour
{
    void Start()
    {

        Display.displays[0].SetRenderingResolution(640, 400);
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();

        }
        if(Display.displays.Length > 1)
        {
            Display.displays[1].SetRenderingResolution(1280, 720);
        }
    }

}
