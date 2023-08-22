using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class ManagerPlayer : MonoBehaviour
    {
        public FirstPersonController player;
        public float vida = 100;
        public float fome = 100;
        public float estamina = 100;
        public float peso = 100;
        
        void Start()
        {
            player = FindObjectOfType<FirstPersonController>();
        }

        
        void Update()
        {

        }

        public void pesadopeso(float _peso)
        {
            _peso = _peso / 1.5f;
            player.targetSpeed -= _peso;
        }

        public void diminuirpeso(float _peso)
        {
            
        }
        
    }
}