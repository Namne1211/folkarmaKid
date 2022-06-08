using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;
    [SerializeField]
    List<obstacle> ObjectData = new List<obstacle>();
    List<int> ObjectIndex = new List<int>();
    public GameObject obstacle;
    public GameObject powerPoint;
    public GameObject holder;
    private void OnEnable()
    {
        AddGridPos();
        InstantObstacle();
    }

    //add to list
    void AddGridPos()
    {
        for (float x = 0; x < size * 4; x += size)
            for (float y = 0; y < size * 4; y += size)
            {
                Vector3 point = GetNearestPointonGrid(new Vector3(x + transform.position.x, y + transform.position.y, transform.position.z));
                ObjectData.Add(new obstacle(point, false));
            }

        for (int i = 0; i < ObjectData.Count; i++)
        {
            ObjectIndex.Add(i);
        }
    }

    void InstantObstacle()
    {
        //random choice for power station
        int firstPower = ObjectIndex[Random.Range(0, ObjectIndex.Count - 1)];
        ObjectIndex.Remove(firstPower);
        int SecondPower = ObjectIndex[Random.Range(0, ObjectIndex.Count - 1)];

        Instantiate(powerPoint, ObjectData[firstPower].position, new Quaternion(0, 0, 0, 0), holder.transform);
        ObjectData[firstPower].Open = true;
        Instantiate(powerPoint, ObjectData[SecondPower].position, new Quaternion(0, 0, 0, 0), holder.transform);
        ObjectData[SecondPower].Open = true;

        //spawn other object
        foreach (obstacle op in ObjectData)
        {
            if (op.Open) continue;
            Instantiate(obstacle, op.position, new Quaternion(0, 0, 0, 0), holder.transform);
        }
    }

    //create a point to follow the parent object
    public Vector3 GetNearestPointonGrid(Vector3 pos)
    {
        pos -= transform.position;
        int xCount = Mathf.RoundToInt(pos.x / size);
        int yCount = Mathf.RoundToInt(pos.y / size);
        int zCount = Mathf.RoundToInt(pos.z / size);


        Vector3 result = new Vector3((float)xCount * size, (float)yCount * size, (float)zCount * size);

        result += transform.position;
        return result;
    }




    public void clearList()
    {
        ObjectIndex.Clear();
        ObjectData.Clear();
    }
}

//class to hold position and state of the obstacle
class obstacle
{
    public Vector3 position;
    public bool Open;
    public obstacle(Vector3 pos, bool open)
    {
        position = pos;
        Open = open;
    }

}
