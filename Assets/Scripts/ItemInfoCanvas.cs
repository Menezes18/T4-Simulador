using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoCanvas : MonoBehaviour
{

    public GameObject player;
    
    public void Awake()
    {
        TransformPlayer();
    }
    void Update()
    {
        
        transform.LookAt(player.transform);
        
    }


    public void TransformPlayer()
    {
        player = GameObject.FindWithTag("Player");
    }
}
