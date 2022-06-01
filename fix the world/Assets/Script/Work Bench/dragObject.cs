using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDestroyHandler(GameObject objectToDestroy);
public class dragObject : MonoBehaviour
{
    //public MinigameManager minigameManager;
    public GameObject targetCursor;
    private Vector3 mOffset;

    private float mZcord;

    private Vector3 startPos;

    public OnDestroyHandler onDestroy;

    //bounderies
    public int horizontalBounderies = 8;

    public int verticalBounderies = 5;

    private void Start()
    {
        //minigameManager = GameObject.Find("GameManager").GetComponent<MinigameManager>();
        //minigameManager.onRunningMinigame += RemoveObj;
        targetCursor = GameObject.Find("mouse");
        startPos = transform.position;
    }

    private void Update()
    {
        if (transform.position.x < -horizontalBounderies || transform.position.x > horizontalBounderies ||
            transform.position.z < -verticalBounderies || transform.position.z > verticalBounderies)
        {
            RemoveObj(1);
        }

    }

    void RemoveObj(int ind)
    {
        onDestroy?.Invoke(gameObject);
        Destroy(gameObject);

    }
    public void MovingStart()
    {
        mZcord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mOffset = gameObject.transform.position - GetMouseWorldPos();

    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = targetCursor.transform.position;

        mousePoint.z = mZcord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void IsMoving()
    {
        //decide moving range
        Vector3 movingRange = GetMouseWorldPos() + mOffset + new Vector3(0, 1, 0);

        transform.position = movingRange;
    }



}
