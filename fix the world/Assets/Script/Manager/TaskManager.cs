using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TaskManager : MonoBehaviour
{

    //public TextMeshProUGUI taskToShow;
    public Image en;
    public Image te;
    public Image hu;
    public Environment trop;
    public tempature temp;
    public humid humid;

    private void Start()
    {
        NewTask();
    }

    private void Update()
    {
       
    }
    void NewTask()
    {

        trop = EnumExtend.RandomEnumValue<Environment>();
        temp = EnumExtend.RandomEnumValue<tempature>();
        humid = EnumExtend.RandomEnumValue<humid>();

        //task icon /////////////////////////////////////////
        switch (trop){
            case Environment.tropical:
                en.sprite = Resources.Load<Sprite>("Art/Other/Icon/tropical");
                break;
            case Environment.artic:
                en.sprite = Resources.Load<Sprite>("Art/Other/Icon/artic");
                break;
            case Environment.desert:
                en.sprite = Resources.Load<Sprite>("Art/Other/Icon/desert");
                break;
            default:
                Debug.Log("task error");
                break;
        }

        switch (humid)
        {
            case humid.wet:
                hu.sprite = Resources.Load<Sprite>("Art/Other/Icon/wet");
                break;
            case humid.moist:
                hu.sprite = Resources.Load<Sprite>("Art/Other/Icon/moist");
                break;
            case humid.dry:
                hu.sprite = Resources.Load<Sprite>("Art/Other/Icon/dry");
                break;
            default:
                Debug.Log("task error");
                break;
        }

        switch (temp)
        {
            case tempature.hot:
                te.sprite = Resources.Load<Sprite>("Art/Other/Icon/hot");
                break;
            case tempature.cold:
                te.sprite = Resources.Load<Sprite>("Art/Other/Icon/cold");
                break;
            case tempature.warm:
                te.sprite = Resources.Load<Sprite>("Art/Other/Icon/warm");
                break;
            default:
                Debug.Log("task error");
                break;
        }
        //task icon /////////////////////////////////////////
    }


}
public static class EnumExtend
{
    public static T RandomEnumValue<T>()
    {
        var values = Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(random);
    }
}
