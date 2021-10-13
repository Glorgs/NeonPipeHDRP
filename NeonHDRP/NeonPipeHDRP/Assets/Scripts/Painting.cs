using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    public float timeBetweenSpawn = 0.1f;
    public GameObject decalPrefab;
    public Transform decalSpawnTransform;

    private float t = 0f;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > timeBetweenSpawn)
        {
            GameObject decal = Instantiate(decalPrefab, decalSpawnTransform.position, Quaternion.AngleAxis(90,Vector3.right));
            decal.transform.localEulerAngles = new Vector3(90 - transform.eulerAngles.z , 90, 0);
            t = 0;
        }
    }
}
