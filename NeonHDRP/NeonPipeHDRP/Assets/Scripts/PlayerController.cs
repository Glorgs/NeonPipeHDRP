using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 1f;
    [SerializeField] private float rotationSpeed = 20f;

    [SerializeField] private float aimingAngle = 90f;
    [SerializeField] private float aimingSpeed = 10f;

    private PlayerInputAction playerAction;
    private InputActionMap actionMap;

    // [SerializeField] InputActionAsset actionAsset;

    //DEBUG : A ENLEVER DANS LA PREFAB FINALE
    public GameObject spray;
    public GameObject aimMin;
    public GameObject aimMax;

    private bool isPainting;

    private float aimCurrentAngle;
    private float aimDistance;
    private float aimLimitMin;
    private float aimLimitMax;

    private void Awake() {
        playerAction = new PlayerInputAction();

        InitializeActionMap();

        actionMap["Paint"].performed += ctx => StartPaint();
        actionMap["Paint"].canceled += ctx => StopPaint();
    }

    private void Start() {
        isPainting = false;

        aimDistance = (spray.transform.position - transform.position).magnitude;

        aimLimitMin = - aimingAngle / 2;
        aimLimitMax = aimingAngle / 2;

        aimMin.transform.RotateAround(transform.position, transform.forward, aimLimitMin);
        aimMax.transform.RotateAround(transform.position, transform.forward, aimLimitMax);
    }


    private void InitializeActionMap() {
        string playerTag = this.tag;
        actionMap = playerAction.asset.FindActionMap(tag);
    }

    private void FixedUpdate()
    {
        Rotation();

        // MoveForward(); A mettre plutôt sur un parent avec les 2 joueurs + la caméra
        Aim();
        Painting();
    }

    private void Rotation() { 
        float moveInput = actionMap["Move"].ReadValue<float>();
        // Debug.Log("wesh");

        Vector3 deltaRotation = new Vector3(0, 0, moveInput * rotationSpeed);
        transform.Rotate(deltaRotation);
    }

    private void MoveForward() {
        transform.position += forwardSpeed * Time.deltaTime * transform.forward;
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

    private void StartPaint() {
        isPainting = true;
    }

    private void StopPaint() {
        isPainting = false;
    }

    private void Painting() {
        if (isPainting) {
            //DEBUG
            spray.transform.RotateAround(spray.transform.position, transform.position - spray.transform.position, 300 * Time.deltaTime);
        }
    }

    private void OnEnable() {
        playerAction.Enable();
    }

    private void OnDisable() {
        playerAction.Disable();
    }

}
