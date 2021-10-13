using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingZone : MonoBehaviour
{
    [SerializeField] private float speedForward;

    private void Update()
    {
        transform.position += speedForward * Time.deltaTime * transform.forward;
    }
}
