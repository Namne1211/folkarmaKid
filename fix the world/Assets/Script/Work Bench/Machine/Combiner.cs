using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combiner : MonoBehaviour
{
    public int productRecieve;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "product")
        {
            productRecieve++;
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (productRecieve >= 3)
        {
            //change screen and play animation
            Debug.Log("win");
        }
    }
}
