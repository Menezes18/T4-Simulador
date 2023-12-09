using System;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform targetWaypoint;
    public float moveSpeed = 3f;

    private void Update()
    {
        //
    }

    public void Start()
    {
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        // Verifica se o waypoint de destino está definido
        if (targetWaypoint != null)
        {
            // Calcula a direção para o waypoint
            Vector3 direction = targetWaypoint.position - transform.position;

            // Normaliza a direção para manter uma velocidade constante
            direction.Normalize();

            // Move o NPC na direção do waypoint
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}