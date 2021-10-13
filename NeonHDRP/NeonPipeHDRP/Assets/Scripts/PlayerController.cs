using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float rotationSpeedIncrease = 0.001f;
    [SerializeField] private float rotationSpeedMax = 40f;

    [SerializeField] private float aimingAngle = 90f;
    [SerializeField] private float aimingSpeed = 3f;
    [SerializeField] private float aimingSpeedIncrease = 0.001f;
    [SerializeField] private float aimingSpeedMax = 15f;

    private PlayerInputAction playerAction;
    private InputActionMap actionMap;

    //DEBUG : A ENLEVER DANS LA PREFAB FINALE
    public GameObject spray;

    private Rigidbody playerRb;

    private float distanceToCenter;

    public bool isPainting;

    private float aimCurrentAngle;
    private float aimDistance;
    private float aimLimitMin;
    private float aimLimitMax;

    private void Awake() {
        playerAction = new PlayerInputAction();

        InitializeActionMap();

        actionMap["Paint"].performed += ctx => PaintingManager();
    }

    private void Start() {
        playerRb = GetComponentInChildren<Rigidbody>();
        isPainting = true;

        InitializeAim();
        // InitializeConstraint();
    }


    private void InitializeActionMap() {
        string playerTag = this.tag;
        actionMap = playerAction.asset.FindActionMap(tag);
    }

    private void FixedUpdate()
    {
        UpdateRotationAndAimSpeed();

        Rotation();
        Aim();
    }

    private void UpdateRotationAndAimSpeed() {
        rotationSpeed = Mathf.Clamp(rotationSpeed + rotationSpeedIncrease * Time.deltaTime, 0, rotationSpeedMax);
        aimingSpeed = Mathf.Clamp(aimingSpeed + aimingSpeedIncrease * Time.deltaTime, 0, aimingSpeedMax);
    }

    private void Rotation() { 
        float moveInput = actionMap["Move"].ReadValue<float>();

        Vector3 deltaRotation = new Vector3(0, 0, moveInput * rotationSpeed);
        transform.Rotate(deltaRotation);
    }

    private void InitializeAim() {
         aimDistance = (spray.transform.position - transform.position).magnitude;

        aimLimitMin = - aimingAngle / 2;
        aimLimitMax = aimingAngle / 2;    
    }

    private void Aim() {
        float aimInput = actionMap["Aim"].ReadValue<float>();
        float direction = Mathf.Sign(aimInput);

        float oldAngle = aimCurrentAngle;

        aimCurrentAngle += aimInput * aimingSpeed;
        aimCurrentAngle = ClampAngle(aimCurrentAngle, aimLimitMin, aimLimitMax);

        float deltaRotation = aimCurrentAngle - oldAngle;

        //DEBUG
        spray.transform.RotateAround(transform.position, transform.forward, deltaRotation);
    }

    public static float ClampAngle(float angle, float min, float max)
     {
         if (angle < -360F)
             angle += 360F;
         if (angle > 360F)
             angle -= 360F;
         return Mathf.Clamp(angle, min, max);
     }

    private void PaintingManager() {
        isPainting = !isPainting;
    }

    private void OnEnable() {
        playerAction.Enable();
    }

    private void OnDisable() {
        playerAction.Disable();
    }

}
