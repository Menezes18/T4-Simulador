using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Debug;
using Unity.VisualScripting;

public class PlantTrigger : MonoBehaviour, IObserverPlanta
{
   
   [SerializeField] private Plant plant;
   [SerializeField] private Estacao estacaoPlanta;
   public bool EstaEstacao = false;
   private float segundos = 0;
   private float multiplacador;
   private float soma = 86400f;

   public int[] aguas;
   [SerializeField] private int idadePlanta = 0;

   private int diaPlanta;
   [SerializeField] private int ciclodia;
   private Transform t;
   public GameObject previousPrefab;
   public void Start()
   {
      
      t = this.transform;
      if (plant == null)
      {
         return;
      }

      diaPlanta = plant.dias;
      multiplacador = 86400f / CicloDiaNoite.ciclo.duracaoDoDia;
      estacaoPlanta = plant.Estacao;
      SubjectPlant.inst.Add(this);
   }
   public void NotifyPlanta(Estacao estacaoAtual)
   {
      if(estacaoPlanta == estacaoAtual)
      {
         EstaEstacao = true;
      }  
      else if(estacaoPlanta != estacaoAtual)
      {
         EstaEstacao = false;
      }
   }

   private void FixedUpdate() {
   
      if(EstaEstacao){
         ciclodiaPlant();
         plantaEstagio();
      }
   }

   public void ciclodiaPlant()
   {
      segundos += Time.deltaTime * multiplacador;
         
         if (segundos >= soma)
         {
               segundos = 0;
               idadePlanta++;
         }
   }


   public void plantaEstagio()
   {
      ciclodia = diaPlanta / 3;
         if (idadePlanta == ciclodia)
         {
            t.position = new Vector3(t.position.x, plant.transform, t.position.z);
            
            GetPrefab(1, t);
         }else if(idadePlanta == ciclodia * 2)
         {
            t.position = new Vector3(t.position.x, plant.transform, t.position.z);
            GetPrefab(2, t);
         }else if(idadePlanta == diaPlanta) 
         {
            t.position = new Vector3(t.position.x, plant.transform, t.position.z);
            GetPrefab(3, t);
         }
   }
      public GameObject GetPrefab(int estagio, Transform parent)
      {
         if (estagio >= 1 && estagio <= 3)
         {
               if (previousPrefab != null)
               {
                  Destroy(previousPrefab);
               }
               GameObject prefab = plant.prefabs[estagio - 1];
               GameObject newPrefab = Instantiate(prefab, parent);
               previousPrefab = newPrefab;
               return newPrefab;
         }
         else
         {
               //Debug.Log("Estágio inválido");
               return null;
         }
      }
   }
