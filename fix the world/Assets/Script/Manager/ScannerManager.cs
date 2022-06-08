using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerManager : MonoBehaviour
{
    GameObject aumentHolder;
    bool spawned;
    public GameObject artic;
    public GameObject desert;
    public GameObject tropical;
    public List<GameObject> ObjToSpawn;
    GameObject toSpawn;
    public int objectLimit = 12;

    private void Start()
    {
        aumentHolder = GameObject.Find("Plant Holder");
    }


    /// <param name="PlantIndex"> 
    /// 1 == artic
    /// 2 == desert
    /// 3 == tropical
    /// </param>
    public void SpawnOnScan(int PlantIndex)
    {
        int augment = ObjToSpawn.Count;

        Debug.Log(augment);
        if (augment < objectLimit)
        {
            
            Vector3 rndPos = new Vector3(Random.Range(-2, 2), aumentHolder.transform.position.y, Random.Range(-3, 0));
            switch (PlantIndex)
            {
                case 1:
                    toSpawn = Instantiate(artic, rndPos, aumentHolder.transform.rotation, aumentHolder.transform);
                    break;
                case 2:
                    toSpawn = Instantiate(desert, rndPos, aumentHolder.transform.rotation, aumentHolder.transform);
                    break;
                case 3:
                    toSpawn = Instantiate(tropical, rndPos, aumentHolder.transform.rotation, aumentHolder.transform);
                    break;
            }
            if (toSpawn == null) return;
            int child = toSpawn.transform.childCount;
            //ObjToSpawn.Add(asd);
            for (int i = 0; i < child; i++)
            {
                ObjToSpawn.Add(toSpawn.transform.GetChild(0).gameObject);
                toSpawn.transform.GetChild(0).gameObject.GetComponent<dragObject>().onDestroy += removemember;
                toSpawn.transform.GetChild(0).parent = aumentHolder.transform;
            }
            Destroy(toSpawn);
            if (toSpawn.GetComponent<dragObject>() != null)
            {
                toSpawn.GetComponent<dragObject>().onDestroy += removemember;
            }
        }

    }

    public void removemember(GameObject objectToRemove)
    {
        ObjToSpawn.Remove(objectToRemove);
    }

    public void RemoveAll()
    {
        if(ObjToSpawn!=null)
        foreach(var obj in ObjToSpawn)
        {
            obj.GetComponent<dragObject>().onDestroy?.Invoke(obj);
        }
    }
}
