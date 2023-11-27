using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerNpc : MonoBehaviour
{
    public string playerTag = "Player";
    public float distanceThreshold = 5f;
    private AudioSource audioSource;
    private bool isPlaying = false;
    private float originalVolume;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalVolume = audioSource.volume;
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player == null)
        {
            Debug.LogWarning("Player not found. Make sure the player has the correct tag.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < distanceThreshold)
        {
            if (!isPlaying)
            {
                audioSource.Play();
                isPlaying = true;
            }

            // Adjust volume based on distance
            float volume = 1f - (distanceToPlayer / distanceThreshold);
            audioSource.volume = Mathf.Clamp(volume, 0f, originalVolume);
        }
        else
        {
            if (isPlaying)
            {
                audioSource.UnPause();
                isPlaying = false;
            }
        }
    }
}

