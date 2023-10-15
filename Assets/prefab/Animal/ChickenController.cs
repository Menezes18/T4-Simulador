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
    private Transform player;
    private Vector3 initialPosition;
    private bool isRunning;
    private bool isSpecialAnimationTriggered;
    private enum ChickenState
    {
        nenhum,
        Walking,
        Running,
        SpecialAnimation
    }
    [SerializeField]
    private ChickenState currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        currentState = ChickenState.Walking;
        isRunning = false;
        isSpecialAnimationTriggered = false;
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case ChickenState.nenhum:
                Idle();
                break;
            case ChickenState.Walking:
                HandleWalkingState();
                break;
            case ChickenState.Running:
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
        if (randomvalue < 1)
        {
           Debug.Log("entrou"); 
            currentState = ChickenState.nenhum;
        }
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, transform.forward, out hit, 1.0f))
        {
            Obstaculo obstaculo = hit.collider.GetComponent<Obstaculo>();
            if (obstaculo != null)
            {
                Debug.Log("VIREI VIU");
                transform.Rotate(0, Random.Range(90, 180), 0);
                
            }
            
        }

        // Check for the player nearby
        if (!isRunning && Physics.CheckSphere(transform.position, runDistance, playerLayer))
        {
            player = FindPlayer();
            if (player != null)
            {
                
                animator.SetBool("Walk", true);
                //isRunning = true;
                //currentState = ChickenState.Running;
            }
        }

        // Move the chicken forward
        animator.SetBool("Walk", true);
        Debug.Log("andei");
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void HandleRunningState()
    {
        if (player != null)
        {
            // Run away from the player
            Vector3 runDirection = transform.position - player.position;
            runDirection.y = 0; // Ensure the chicken stays on the same level
            runDirection.Normalize();
            transform.position += runDirection * moveSpeed * Time.deltaTime;
        }

        // Check if the chicken has escaped
        if (Vector3.Distance(transform.position, initialPosition) >= runDistance)
        {
            isRunning = false;
            currentState = ChickenState.Walking;
        }
    }

    private Transform FindPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, runDistance, playerLayer);
        if (colliders.Length > 0)
        {
            return colliders[0].transform;
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(raycastOrigin.position, transform.forward * 1.0f);
    }
}
