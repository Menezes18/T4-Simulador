using System.Collections;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public string playerTag = "Player";
    public float distanceThreshold = 5f;
    private AudioSource audioSource;
    private bool isPlaying = false;
    private float originalVolume;

    public Animator anim;

    public float audio = 0f;

    private float waypointTimer = 5f; // 60 seconds for 1 minute
    private float waypointCooldown = 0f;
    private float chanceToCallWaypoint = 0.2f;
    public WayPoint pontoInicial;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalVolume = audioSource.volume;
    }

    void Update()
    {
        // Check waypoint timer
        waypointCooldown -= Time.deltaTime;
        if (waypointCooldown <= 0f)
        {
            // Reset the timer
            waypointCooldown = waypointTimer;

            // Check the chance to call waypoint
            //if (Random.Range(0f, 1f) <= chanceToCallWaypoint)
            //{
                CallWaypoint();
           // }
        }

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
                anim.SetTrigger("tchau");
                transform.LookAt(player.transform.position);
                audioSource.Play();
                isPlaying = true;
            }

            if (audio >= audioSource.clip.length)
            {
                audioSource.Stop();
                return;
            }

            audio += Time.deltaTime;

            float volume = 1f - (distanceToPlayer / distanceThreshold);
            audioSource.volume = Mathf.Clamp(volume, 0f, originalVolume);
        }
        else
        {
            if (isPlaying)
            {
                // transform.LookAt(player.transform.position);
                audioSource.Stop();

                isPlaying = false;
            }
        }
    }

    void CallWaypoint()
    {
        WayPoint waypointDoInimigo = GetComponent<WayPoint>();

        // Iniciar movimentação para o próximo waypoint
        StartCoroutine(MoverParaProximoWaypoint( waypointDoInimigo != null ? waypointDoInimigo : pontoInicial));
    }
    
    
    IEnumerator MoverParaProximoWaypoint(WayPoint pontoInicial)
    {
        var velocidade = 2;
        WayPoint currentWaypoint = pontoInicial;

        // while (currentWaypoint != null)
        // {
            transform.LookAt(currentWaypoint.transform.position);
            transform.Translate(Vector3.forward * velocidade * Time.deltaTime);

            // Verificar se o inimigo chegou ao waypoint
            float distancia = Vector3.Distance(transform.position, currentWaypoint.transform.position);
            if (distancia < 0.1f)
            {
                // Atualizar o próximo waypoint
                currentWaypoint = currentWaypoint.next;

                // Se houver próximo waypoint, continuar movendo
                if (currentWaypoint == null)
                {
                   // break; // Saia do loop se não houver mais waypoints
                }
            }

            // Aguardar um frame antes de verificar novamente
            yield return null;
       // }
    }
}
