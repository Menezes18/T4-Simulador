using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChickenController : MonoBehaviour
{
    public Transform raycastOrigin;
    public LayerMask playerLayer;
    public float moveSpeed = 2.0f;
    public float turnChance = 10; // 10% de chance de virar
    public float specialAnimationChance = 10; // 10% de chance de chamar uma animação especial
    public float runDistance = 5.0f;

    public Animator animator;
    public Transform player;
    
    private Vector3 initialPosition;
    private bool isRunning;
    public bool playerBool = false;
    public GameObject itemPrefab;
    public enum ChickenState
    {
        nenhum,
        Walking,
        Player,
        SpecialAnimation
    }
    [NonSerialized]
    public ChickenState currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        currentState = ChickenState.Walking;
        isRunning = false;
        InvokeRepeating("DropItem", 0f, 300f);
    }
    private void DropItem()
    {
        if (itemPrefab != null)
        {
            // Instantiate the itemPrefab at the chicken's position
            GameObject droppedItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);

            // Optionally, you can add logic to modify the dropped item's properties or behavior here

            Debug.Log("Item dropped!");
        }
        else
        {
            Debug.LogWarning("Item prefab not assigned to ChickenController!");
        }
    }
    private void FixedUpdate()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.transform;
        switch (currentState)
        {
            case ChickenState.nenhum:
                Idle();
                break;
            case ChickenState.Walking:
                HandleWalkingState();
                break;
            case ChickenState.Player:
                HandleRunningState();
                break;
            case ChickenState.SpecialAnimation:
                
                break;
        }
    }

    private void Idle()
    {
        animator.SetBool("Eat", true);
    }

    public void EventAnimationEat()
    {
        Debug.Log("TESTE");
        animator.SetBool("Eat", false);
        currentState = ChickenState.Walking;
    
        
    }
    
    private void HandleWalkingState()
    {
        int randomvalue = Random.Range(0, 200);
        Debug.Log(randomvalue);
        
        
        if (randomvalue < 2 && !playerBool)
        {
           
            currentState = ChickenState.nenhum;
        }
    
        if (randomvalue < 1)
        {
            transform.Rotate(0, Random.Range(90, 180), 0);
        }
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, transform.forward, out hit, 1.0f))
        {
            Obstaculo obstaculo = hit.collider.GetComponent<Obstaculo>();
            if (obstaculo != null)
            {
                
                transform.Rotate(0, Random.Range(90, 180), 0);
                
            }
            
        }

        // Move the chicken forward
        animator.SetBool("Walk", true);
        Debug.Log("andei");
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void HandleRunningState()
    {
        
        float stopDistance = 1f;
        if (player != null && playerBool)
        {
            // Direção para o jogador
            Vector3 runDirection = (player.position - transform.position).normalized;

            // Verifica a distância para decidir se a galinha deve parar ou seguir
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= stopDistance)
            {
                animator.SetBool("Walk", false);
                // O jogador está muito próximo, pare de seguir
                
               // currentState = ChickenState.Walking;
            }
            else
            {
                animator.SetBool("Walk", true);
                // Rotaciona a galinha para olhar na direção do jogador
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(runDirection), 5.0f * Time.deltaTime);

                // Move a galinha na direção do jogador
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            
        }
        if (!playerBool)
        {
            currentState = ChickenState.Walking;
        }

    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(raycastOrigin.position, transform.forward * 1.0f);
    }
}
