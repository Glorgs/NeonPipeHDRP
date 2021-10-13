using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private float distanceToCenter;
    private Quaternion initialRotation;

    private void Start() {
        distanceToCenter = (transform.position - transform.parent.position).magnitude;
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        ConstraintToCircle();
    }
    private void ConstraintToCircle() {
        transform.position = transform.parent.position + distanceToCenter * (-transform.parent.up);
        transform.localRotation = initialRotation;
    }
}
