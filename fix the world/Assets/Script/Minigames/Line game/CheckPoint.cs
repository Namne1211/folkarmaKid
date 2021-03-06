using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public MinigameManager minigameManager;
    public GameObject holder;
    public GameObject line;
    
    Vector3 prevPos;
    public int point;
    private void Start()
    {
        transform.parent = holder.transform;
    }
    private void Update()
    {
        transform.LookAt(holder.transform, new Vector3(0, 1, 0));


        win();

        
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (other.gameObject.name == "line")
            {
                Debug.Log("check");
                RenewPos();
                line.GetComponent<IndicateLine>().speed += 40f;
                line.GetComponent<IndicateLine>().direction = -line.GetComponent<IndicateLine>().direction;


            }

        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (other.gameObject.name == "line")
            {
                Debug.Log("check");
                RenewPos();
                line.GetComponent<IndicateLine>().speed += 40f;
                line.GetComponent<IndicateLine>().direction = -line.GetComponent<IndicateLine>().direction;
            }

        }

    }

    void RenewPos()
    {

        point += 1;
        //float holderLength = (holder.transform.lossyScale.x - transform.lossyScale.x) / 2;
        
        float radius = 0.3f;
        float angle = Random.Range(0, 360);
        Vector3 randomCircle = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius,0 , Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
        //Vector3 worldPos = transform.TransformPoint(randomCircle * radius);
        transform.localPosition = randomCircle;
    }

    void win()
    {
        if (point >= 5)
        {
            minigameManager.onMinigameDone?.Invoke(2);           
        }
    }
}
