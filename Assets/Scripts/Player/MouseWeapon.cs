using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool canUnlock = true;

    [SerializeField]
    private float sensitivity = 5f;

    //[SerializeField]
    //private int smoothSteps = 10;

    [SerializeField]
    private float smoothWeight = 0.4f;

    [SerializeField]
    private float rollAngle = 10f;

    [SerializeField]
    private float rollSpeed = 3f;

    [SerializeField]
    private Vector2 defaultLookLimits = new Vector2(-70f, 80f);

    private Vector2 lookAngles;

    private Vector2 currentMouseLook;
    private Vector2 smoothMove;

    private float currentRollAngle;

    private int lastLookFrame;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LockUnlockCursor();

        if(Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    void LockUnlockCursor() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround ()
    {
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.mouseY), Input.GetAxis(MouseAxis.mouseX));

        lookAngles.x += currentMouseLook.x * sensitivity * (invert ? 1f : -1f); //by default invert is turned on, good practice
        lookAngles.y += currentMouseLook.y * sensitivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);

        currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw(MouseAxis.mouseX) * rollAngle, Time.deltaTime * rollSpeed);

        //print("Valuse is " + Input.GetAxis("Horizontal"));
        //print("Valuse is " + Input.GetAxisRaw("Horizontal")); debug

        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, currentRollAngle);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f); 

    }
}
