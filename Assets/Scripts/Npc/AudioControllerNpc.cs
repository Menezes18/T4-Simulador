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

    public Animator anim;

    public float audio;

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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Chamar"))
        {
            transform.LookAt(player.transform.position);
        }
        if (distanceToPlayer < distanceThreshold)
        {
            if (!isPlaying)
            {
                audioSource.time = audio;
                audioSource.Play();
                isPlaying = true;
                anim.SetTrigger("Andar");
                //audio = audioSource.clip.length;
            }

            if(audio >= audioSource.clip.length)
            {
                audioSource.Stop();
                return;
            }

                audio += Time.deltaTime;
            // Adjust volume based on distance
            float volume = 1f - (distanceToPlayer / distanceThreshold);
            audioSource.volume = Mathf.Clamp(volume, 0f, originalVolume);
        }
        else
        {
            if (isPlaying)
            {
                transform.LookAt(player.transform.position);
                anim.SetTrigger("Chamar");
                audioSource.UnPause();
                
                isPlaying = false;
            }
        }
    }
}

