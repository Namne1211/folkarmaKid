using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistanceCulling : MonoBehaviour
{
    [SerializeField]
    private float[] distances;

    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        camera.layerCullDistances = distances;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
