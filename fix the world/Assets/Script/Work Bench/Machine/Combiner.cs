using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;
public class Combiner : MonoBehaviour
{
    public UnityEvent CombinerEvent;
    public GameObject panel;
    public int productRecieve;
    public static int playtime;
    int currentPlay;
    public TMPro.TextMeshProUGUI textMeshPro;

    public UnityEvent CompleteEvent;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "product")
        {           
            Destroy(other.gameObject);
            productRecieve++;
        }
    }


    private void Start()
    {
        currentPlay = PlayerPrefs.GetInt("playTime");
        playtime = currentPlay;
    }
    private void Update()
    {
        if (productRecieve >= 3)
        {
            
            StartCoroutine(ResetgGameState());
            //change screen and play animation
            
            CombinerEvent.Invoke();
}
        textMeshPro.text = PlayerPrefs.GetInt("playTime").ToString();
    }

    IEnumerator ResetgGameState()
    {
        panel.SetActive(true);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
        playtime++;
        PlayerPrefs.SetInt("playTime", Combiner.playtime);
        SceneManager.LoadScene(0);
    }
}
