using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicateLine : MonoBehaviour
{
    public GameObject cylinder;
    Vector3 movingDir;
    public float speed = 50f;
    public Vector3 direction = Vector3.forward;

    private void Start()
    {
        movingDir = Vector3.left * 3;
    }
    private void Update()
    {
        // float holderLength = (holder.transform.lossyScale.x - transform.lossyScale.x) / 2;
        // if (transform.position.x >= holderLength || transform.position.x <= -holderLength)
        // {
        //     movingDir = -movingDir;
        //  }
        transform.RotateAround(cylinder.transform.position, direction, speed * Time.deltaTime);
        //transform.Translate(movingDir * Time.deltaTime);
    }
}
