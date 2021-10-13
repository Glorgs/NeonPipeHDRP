using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLeave : MonoBehaviour
{
    public float timeToDestroy = 1f;

    private float t = 0f;

    private void Update()
    {
        t += Time.deltaTime;
        if(t > timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
