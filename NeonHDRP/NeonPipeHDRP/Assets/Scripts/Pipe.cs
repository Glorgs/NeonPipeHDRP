using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    public Transform endPoint;
    public List<GameObject> listTag;
    public List<GameObject> obstacle;
    // Start is called before the first frame update
    void Start()
    {
        listTag = new List<GameObject>();
        obstacle = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
