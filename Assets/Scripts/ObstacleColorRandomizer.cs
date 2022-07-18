using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleColorRandomizer : MonoBehaviour
{
    public List<Color32> colors = new List<Color32>(5);

    public List<GameObject> childrens = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Color randColor = colors[Random.Range(0, colors.Count)];
        foreach (var child in childrens)
        {
            child.gameObject.GetComponent<MeshRenderer>().material.color = randColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}