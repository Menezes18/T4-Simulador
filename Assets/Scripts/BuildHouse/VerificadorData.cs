using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VerificadorData : MonoBehaviour
{
    public BuildTool Tool;
    public BuildHouse build;
    public HouseData data;
    private HotbarDisplay inventory;
    


    private void Start()
    {
        inventory = FindObjectOfType<HotbarDisplay>();
        Tool = FindObjectOfType<BuildTool>();
        
    }

    private void Update()
    {
        CheckRaycast();
        
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            construir(data);
        }
    }

    public void CheckRaycast()
    {
        RaycastHit hitInfo;
        if (Tool.IsRayHittingSomething(Tool._buildModeLayerMask, out hitInfo))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
           // Debug.Log("Raycast hit: " + hitObject.name);

            // Check if the hit object has the specific script
            BuildHouse buildData = hitObject.GetComponent<BuildHouse>();
            build = buildData;
            
            if (buildData != null)
            {
                data = buildData.housedata;
                buildData.hud();

            }
        }
        else
        {
            //Debug.Log("Raycast hit nothing.");
        }
    }

    public void construir(HouseData buildData)
    {
        
        foreach (HouseData.BuildComponent buildComponent in buildData.build)
                {
                    foreach (ResourceRequirement requirement in buildComponent.resourceRequirements)
                    {
                        if (inventory.CheckItemInHotbar(requirement.item.ID))
                        {
                            Debug.Log("Construindo componente: " + buildComponent.name);
                            removerRecursos(requirement.item);
                        }
                        else
                            { 
                                    Debug.Log("Recursos insuficientes para construir: " + buildComponent.name);
                            }
                }
        }
        
    }
        public void removerRecursos(InventoryItemData item)
        { 
           inventory.RemoveItem(item.ID,1);
           build.removeritem(item, 1);
            
        }
}