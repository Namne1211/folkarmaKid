using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScannerManager : MonoBehaviour
{
    GameObject aumentHolder;
    bool spawned;
    public GameObject artic;
    public GameObject desert;
    public GameObject tropical;
    public GameObject artic2;
    public GameObject desert2;
    public GameObject tropical2;
    public List<GameObject> ObjToSpawn;
    GameObject toSpawn;
    public int objectLimit = 12;
    public UnityEvent WinEvent;

    private void Start()
    {
        aumentHolder = GameObject.Find("Plant Holder");
    }


    /// <param name="PlantIndex"> 
    /// 1 == artic
    /// 2 == desert
    /// 3 == tropical
    /// 4 == artic
    /// 5 == desert
    /// 6 == tropical
    /// </param>
    public void SpawnOnScan(int PlantIndex)
    {
        int augment = ObjToSpawn.Count;

        Debug.Log(augment);
        if (augment < objectLimit)
        {
            WinEvent.Invoke();
            Vector3 rndPos = new Vector3(Random.Range(-2, 2), aumentHolder.transform.position.y, Random.Range(-3, 0));
            switch (PlantIndex)
            {
                case 1:
                    toSpawn = Instantiate(artic, rndPos, new Quaternion(0,180,0,0), aumentHolder.transform);
                    break;
                case 2:
                    toSpawn = Instantiate(artic2, rndPos, new Quaternion(0, 180, 0, 0), aumentHolder.transform);
                    break;
                case 3:
                    toSpawn = Instantiate(desert, rndPos, new Quaternion(0, 180, 0, 0), aumentHolder.transform);
                    break;
                case 4:
                    toSpawn = Instantiate(desert2, rndPos, new Quaternion(0, 180, 0, 0), aumentHolder.transform);
                    break;
                case 5:
                    toSpawn = Instantiate(tropical, rndPos, new Quaternion(0, 180, 0, 0), aumentHolder.transform);
                    break;
                case 6:
                    toSpawn = Instantiate(tropical2, rndPos, new Quaternion(0, 180, 0, 0), aumentHolder.transform);
                    break;
            }
            if (toSpawn == null) return;

            ObjToSpawn.Add(toSpawn.transform.gameObject);
            toSpawn.gameObject.GetComponent<dragObject>().onDestroy += removemember;
            

        }

    }

    //remove member of object count
    public void removemember(GameObject objectToRemove)
    {
        ObjToSpawn.Remove(objectToRemove);
        Debug.Log(ObjToSpawn.Count);
    }

}
