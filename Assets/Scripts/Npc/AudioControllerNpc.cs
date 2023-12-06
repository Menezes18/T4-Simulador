using System.Collections;
using UnityEngine;

public class AudioControllerNpc : MonoBehaviour
{
    public string playerTag = "Player";
    public float distanceThreshold = 5f;
    public Transform[] waypoints;
    public AudioClip[] audioClips;  // Array de clipes de áudio associados a cada waypoint
    private AudioSource audioSource;
    private bool isPlaying = false;
    private int currentWaypointIndex = 0;
    private float originalVolume;

    public Animator anim;

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
            Debug.LogWarning("Jogador não encontrado. Certifique-se de que o jogador tem a tag correta.");
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
                audioSource.Stop();  
                PlayNextAudioClip();
            }

            if (audioSource.isPlaying)
            {
                float volume = 1f - (distanceToPlayer / distanceThreshold);
                audioSource.volume = Mathf.Clamp(volume, 0f, originalVolume);
            }
            else
            {
                isPlaying = false;
                currentWaypointIndex++;
                if (currentWaypointIndex < waypoints.Length)
                {
                    anim.SetTrigger("Andar");
                    MoveToWaypoint();
                    PlayNextAudioClip();  // Reproduza o próximo clipe de áudio para o novo waypoint
                }
            }
        }
        else
        {
            if (isPlaying)
            {
                transform.LookAt(player.transform.position);
                anim.SetTrigger("Chamar");
                audioSource.Pause();
                isPlaying = false;
            }
        }
    }

    void PlayNextAudioClip()
    {
        if (currentWaypointIndex < waypoints.Length && currentWaypointIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[currentWaypointIndex];
            audioSource.Play();
            isPlaying = true;
        }
    }

    void MoveToWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            transform.LookAt(waypoints[currentWaypointIndex].position);
        }
    }
}
