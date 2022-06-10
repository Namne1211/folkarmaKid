using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Combiner : MonoBehaviour
{
    public GameObject panel;
    public int productRecieve;
    public static int playtime;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "product")
        {           
            Destroy(other.gameObject);
            productRecieve++;
        }
    }

    private void Update()
    {
        if (productRecieve >= 6)
        {
            StartCoroutine(ResetgGameState());
            //change screen and play animation
            playtime++;
            
            Debug.Log("win");

        }
    }

    IEnumerator ResetgGameState()
    {
        panel.SetActive(true);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(0);
    }
}
