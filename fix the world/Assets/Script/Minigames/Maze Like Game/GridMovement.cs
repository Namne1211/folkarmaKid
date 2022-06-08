using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public MinigameManager minigameManager;

    //set up grid moving
    [SerializeField]
    float moveSpeed = 0.25f;
    [SerializeField]
    float snapDistance = 0.25f;
    [SerializeField]
    float movingtiles = 4f;

    //setup line renderer
    [SerializeField]
    GameObject Grid;
    [SerializeField]
    GameObject part;
    [SerializeField]
    LineRenderer lr;
    Vector3 targetPosition;
    Vector3 startPosition;
    Vector3 OriginPos;
    bool moving;
    [SerializeField]
    List<GameObject> partList = new List<GameObject>();
    public GameObject holder;



    //setup swipe input
    Vector3 StartTouchPos;
    Vector3 CurrentTouchPos;
    Vector3 EndTouchPos;
    float swipeRange = 10;
    float tapRange = 5;
    bool stopTouch;

    //setup winning state
    bool Winning;
    public int powerCount;

    private void OnEnable()
    {
        stopTouch = true;
        wait();
        //setup starting line
        lr.useWorldSpace = false;
        startPosition = transform.localPosition;
        OriginPos = transform.localPosition;
        lr.SetPosition(0, OriginPos);
        lr.SetPosition(1, OriginPos);
    }

    IEnumerator wait()
    {
       

        yield return new WaitForSeconds(1);

        stopTouch = false;
    }
    void Update()
    {
        //recieve swipe input
        if (!moving)
            swipe();



        //moving with input
        if (moving)
        {

            if (targetPosition.x <= -movingtiles * 2.5 || targetPosition.x >= movingtiles * 2.5 ||
                targetPosition.z <= -movingtiles * 2.5 || targetPosition.z >= movingtiles * 2.5)
            {
                stopTouch = false;
                moving = false;
                return;
            }

            if (Vector3.Distance(startPosition, transform.localPosition) > snapDistance)
            {
                transform.localPosition = targetPosition;

                //rendering line and update at tartget
                SetupLine();
                startPosition = targetPosition;
                //stopTouch = false;
                moving = false;
                //Debug.Log(moving);
                return;
            }

            lr.SetPosition(lr.positionCount - 1, transform.localPosition);
            transform.localPosition += (targetPosition - startPosition) * moveSpeed * Time.deltaTime;

            return;
        }


    }

    void SetupLine()
    {
        lr.positionCount += 1;
        lr.SetPosition(lr.positionCount - 1, targetPosition);
        if (lr.positionCount > 1)
            lr.SetPosition(lr.positionCount - 2, targetPosition);

        GameObject newtile = Instantiate(part, holder.transform);
        newtile.transform.localPosition = startPosition;
        partList.Add(newtile);
        lr.SetPosition(lr.positionCount - 3, startPosition);
    }

    //reset line
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Line")//line
        {
            resetGameState();

        }
        else if (other.tag == "Power")//powerpoint
        {
            PowerStation power = other.gameObject.GetComponent<PowerStation>();
            if (!power.IsTouch)
            {
                power.IsTouch = true;
                powerCount += 1;
                Debug.Log(powerCount);
            }


        }
        else if (other.tag == "finalPoint")//endstate
        {
            if (powerCount >= 2)
            {
                minigameManager.onMinigameDone?.Invoke(0);
            }
            else
            {
                resetGameState();
            }
        }
    }

    private void resetGameState()
    {
        
        stopTouch = false;

        //reset line
        foreach (GameObject part in partList)
        {
            Destroy(part);
        }
        partList.Clear();
        transform.localPosition = OriginPos;
        lr.positionCount = 2;
        startPosition = OriginPos;
        lr.SetPosition(0, OriginPos);
        lr.SetPosition(1, OriginPos);
        //stop movement
        moving = false;
        //reset game condition
        powerCount = 0;
        GameObject[] powerList = GameObject.FindGameObjectsWithTag("Power");
        foreach (GameObject power in powerList)
        {
            power.GetComponent<PowerStation>().IsTouch = false;
        }
    }

    //swipe input
    void swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartTouchPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            CurrentTouchPos = Input.mousePosition;
            Vector3 Distance = CurrentTouchPos - StartTouchPos;
            if (!stopTouch)
            {
                if (Distance.x < -swipeRange)
                {
                    targetPosition = transform.localPosition + Vector3.left * movingtiles;

                    moving = true;
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange)
                {
                    targetPosition = transform.localPosition + Vector3.right * movingtiles;

                    moving = true;
                    stopTouch = true;
                }
                else if (Distance.y < -swipeRange)
                {
                    targetPosition = transform.localPosition + Vector3.back * movingtiles;

                    moving = true;
                    stopTouch = true;
                }
                else if (Distance.y > swipeRange)
                {
                    targetPosition = transform.localPosition + Vector3.forward * movingtiles;

                    moving = true;
                    stopTouch = true;
                }

            }
        }
           if (Input.touchCount<1)
        {
            stopTouch = false;
            EndTouchPos = Input.mousePosition;

            Vector3 Distance = EndTouchPos - StartTouchPos;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {
                Debug.Log("tap");
            }
        }
    }

    private void OnDisable()
    {
        resetGameState();
    }
}
