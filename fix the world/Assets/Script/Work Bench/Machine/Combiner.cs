using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;
public class Combiner : MonoBehaviour
{
    public UnityEvent CombinerEvent;
    public int productRecieve;
    public static int playtime;
    int currentPlay;
    public TMPro.TextMeshProUGUI textMeshPro;
    public List<GameObject> lights;
    
    public GameObject spinParticle;
    public GameObject winParticle;

    public UnityEvent CompleteEvent;
    private void OnTriggerEnter(Collider other)
    {
        if(lights.Count >= 3)
        switch (other.tag)
        {
            case "EnProduct":                 
                    productRecieve++;
                    lights[0].SetActive(true);
                    lights[3].SetActive(true);
                    Destroy(other.gameObject);
                    break;
            case "HuProduct":
                    productRecieve++;
                    lights[1].SetActive(true);
                    lights[4].SetActive(true);
                    Destroy(other.gameObject);
                    break;
            case "TeProduct":
                    productRecieve++;
                    lights[2].SetActive(true);
                    lights[5].SetActive(true);
                    Destroy(other.gameObject);
                    break;
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
        spinParticle.SetActive(true);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
        spinParticle.SetActive(false);
        winParticle.SetActive(true);
        yield return new WaitForSeconds(5);
        playtime++;
        PlayerPrefs.SetInt("playTime", Combiner.playtime);
        SceneManager.LoadScene(0);
    }
}
