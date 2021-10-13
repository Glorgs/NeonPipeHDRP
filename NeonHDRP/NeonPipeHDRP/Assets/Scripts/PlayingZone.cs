using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingZone : MonoBehaviour
{
    [SerializeField] private float speedForward;

    [SerializeField] private float speedIncrease;
    [SerializeField] private float maxSpeed;

    private float initialSpeed;

    private void Start() {
        initialSpeed = speedIncrease;
    }

    private void Update()
    {
        transform.position += speedForward * Time.deltaTime * transform.forward;

        speedForward = Mathf.Clamp(speedForward + Time.deltaTime, 0, maxSpeed);
    }
}
