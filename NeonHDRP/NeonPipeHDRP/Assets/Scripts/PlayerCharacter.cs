using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float movingAngle = 30f;
    private float distanceToCenter;
    private Vector3 initialRotation;
    private float moveDirection;
    private float currentYAngle;

    private PlayerInputAction playerAction;
    private InputActionMap actionMap;


    private void Awake() {
        playerAction = new PlayerInputAction();
        InitializeActionMap();

        actionMap["Move"].performed += ctx => StartMoving();
        actionMap["Move"].canceled += ctx => StopMoving();
    }
    private void Start() {
        distanceToCenter = (transform.position - transform.parent.position).magnitude;
        initialRotation = transform.localRotation.eulerAngles;
        currentYAngle = initialRotation.y;
    }

    private void Update()
    {
        ConstraintToCircle();
    }

    private void InitializeActionMap() {
        string playerTag = this.tag;
        actionMap = playerAction.asset.FindActionMap(tag);
    }

    private void StartMoving() {
        Debug.Log("StartMoving");
        moveDirection = Mathf.Sign(actionMap["Move"].ReadValue<float>());
        currentYAngle = initialRotation.y + moveDirection * movingAngle;
    }

    private void StopMoving() {
        currentYAngle = initialRotation.y;
    }

    private void ConstraintToCircle() {
        transform.position = transform.parent.position + distanceToCenter * (-transform.parent.up);
        transform.localRotation = Quaternion.Euler(new Vector3(initialRotation.x, currentYAngle, initialRotation.z));
    }

    private void OnEnable() {
        playerAction.Enable();
    }

    private void OnDisable() {
        playerAction.Disable();
    }
}
