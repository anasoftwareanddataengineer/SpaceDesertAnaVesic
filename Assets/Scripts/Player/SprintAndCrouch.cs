using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAndCrouch : MonoBehaviour
{

    private PlayerMovement playerMovement;

    public float sprintSpeed = 24f;
    public float moveSpeed = 12f;
    public float crouchSpeed = 3f;

    private Transform lookTransform;
    private float standHeight = 118.98f;
    private float crouchHeight = 109.98f;

    private bool isCrouching;

    private Footsteps footsteps;

    private float sprintVol = 1f;
    private float crouchVol = 0.1f;
    private float walkVolMin = 0.2f, walkVolMax = 0.6f;

    private float walkStepDist = 0.4f;
    private float sprintStepDist = 0.25f;
    private float crouchStepDist = 0.5f;

    private PlayerStats playerStats;

    private float sprintValue = 100f;
    private float sprintThreshold = 10f;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        lookTransform = transform.GetChild(0);

        footsteps = GetComponentInChildren<Footsteps>();

        playerStats = GetComponent<PlayerStats>();
    }
    // Start is called before the first frame update
    void Start()
    {

        footsteps.volumeMin = walkVolMin;
        footsteps.volumeMax = walkVolMax;
        footsteps.stepDist = walkStepDist;

    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        if(sprintValue > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching && moveSpeed>0)
            {
                playerMovement.speed = sprintSpeed;

                footsteps.stepDist = sprintStepDist;
                footsteps.volumeMin = sprintVol;
                footsteps.volumeMax = sprintVol;
            }
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = moveSpeed;

            footsteps.stepDist = walkStepDist;
            footsteps.volumeMin = walkVolMin;
            footsteps.volumeMax = walkVolMax;

        }

        if(Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            sprintValue -= sprintThreshold * Time.deltaTime;

            if(sprintValue <= 0f)
            {
                sprintValue = 0f;

                playerMovement.speed = moveSpeed;
                footsteps.stepDist = walkStepDist;
                footsteps.volumeMin = walkVolMin;
                footsteps.volumeMax = walkVolMax;

            }

            playerStats.DisplayStaminaStats(sprintValue);
        } else
        {
            if(sprintValue != 100)
            {
                sprintValue += (sprintThreshold / 2f) * Time.deltaTime;

                playerStats.DisplayStaminaStats(sprintValue);

                if(sprintValue > 100f)
                {
                    sprintValue = 100f;
                }
            }
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                lookTransform.localPosition = new Vector3(0f, standHeight, 0f);
                playerMovement.speed = moveSpeed;

                footsteps.stepDist = walkStepDist;
                footsteps.volumeMin = walkVolMin;
                footsteps.volumeMax = walkVolMax;

                isCrouching = false;
            } else
            {
                lookTransform.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;

                footsteps.stepDist = crouchStepDist;
                footsteps.volumeMin = crouchVol;
                footsteps.volumeMax = crouchVol;

                isCrouching = true;
            }
        }
    }
}
