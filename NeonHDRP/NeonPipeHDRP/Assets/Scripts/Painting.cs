using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    public float timeBetweenSpawn = 0.1f;
    public GameObject decalPrefab;
    public Transform decalSpawnTransform;

    private float t = 0f;
    private GameObject peinture;

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > timeBetweenSpawn)
        {
            peinture = Instantiate(decalPrefab, decalSpawnTransform.position, Quaternion.AngleAxis(90,Vector3.right));
            peinture.transform.localEulerAngles = new Vector3(90 - transform.eulerAngles.z , 90, 0);
            CheckIfInTag();

            t = 0;
        }
    }

    void CheckIfInTag()
    {
        Vector3 projectionPeinture = peinture.transform.position + peinture.transform.forward * 7.5f;
        foreach(GameObject obj in PipeManager.chunksPipe)
        {
            foreach (GameObject tag in obj.GetComponent<Pipe>().listTag)
            {
                Transform t = tag.transform;

                Vector3 newProjectPeinture = projectionPeinture - t.position;
                float up = Vector3.Dot(newProjectPeinture, t.up);
                float forward = Vector3.Dot(newProjectPeinture, t.forward);
                float right = Vector3.Dot(newProjectPeinture, t.right);

                Debug.Log(up + " / " + forward + " / " + right);

                if (Mathf.Abs(up) < 10 && Mathf.Abs(right) < 10 && forward > 0)
                {
                    Debug.Log("J'ai score");
                }
            }
        }
    }
}
