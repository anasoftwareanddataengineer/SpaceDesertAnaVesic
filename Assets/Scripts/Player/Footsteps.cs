using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    private AudioSource footstepSound;

    [SerializeField]
    private AudioClip[] footstepClip;

    private CharacterController characterController;

    [HideInInspector]
    public float volumeMin, volumeMax;

    private float accumulatedDist;

    private float wait = 0.001f;

    [HideInInspector]
    public float stepDist;

    void Awake()
    {
        footstepSound = GetComponent<AudioSource>();

        characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        checkToPlayFootsptepSound();
    }

    void checkToPlayFootsptepSound()
    {
        if (!characterController.isGrounded)
          return;

        if(characterController.velocity.sqrMagnitude > 0)
        {
            accumulatedDist += Time.deltaTime;

            if(accumulatedDist > stepDist)
            {
                footstepSound.volume = Random.Range(volumeMin, volumeMax);
                footstepSound.clip = footstepClip[Random.Range(0, footstepClip.Length)];
                footstepSound.Play();

                accumulatedDist = 0f;
            }
        } else
        {
            accumulatedDist = 0f;
        }
    }
}
