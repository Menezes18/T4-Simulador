using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class BuildTool : MonoBehaviour
{
    [SerializeField] private float _rotateSnapAngle = 10f;
    [SerializeField] private float _rayDistance;
    [SerializeField] public LayerMask _buildModeLayerMask;
    [SerializeField] private LayerMask _deleteModeLayerMask;
    [SerializeField] private LayerMask FarmLayer;
    [SerializeField] private int _defaultLayerInt = 8;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private Material _buildingMatPositive;
    [SerializeField] private Material _buildingMatNegative;

    private bool _deleteModeEnabled;

    public bool buildAtivar = false;

    public Camera _camera;

    private List<Vector3> fencePositions = new List<Vector3>();
    
    
    public LayerMask terraArada;
    public BuildingData data;
    public BuildingData placedata;
    public Building _spawnedBuilding;
    private Building _targetBuilding;
    private Quaternion _lastRotation;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    
    public Material materialTerraArada;
    public Material materialTerraNormal;
    public Material materialTerraPronta;
    

    public bool plantio;
    private void Start()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _camera = Camera.main;
        ChoosePart(placedata);
    }

    private void OnEnable()
    {
        BuildingPanelUI.OnPartChosen += ChoosePart;
    }
    
    private void OnDisable()
    {
        BuildingPanelUI.OnPartChosen -= ChoosePart;
    }

    public void ChoosePart(BuildingData data)
    {
        if (data == null)
        {
            return;
        }
        if (_deleteModeEnabled)
        {
            if (_targetBuilding != null && _targetBuilding.FlaggedForDelete) _targetBuilding.RemoveDeleteFlag();
            _targetBuilding = null;
            _deleteModeEnabled = false;
        }

        DeleteObjectPreview();

        var go = new GameObject
        {
            layer = _defaultLayerInt,
            name = "Build Preview"
        };

        _spawnedBuilding = go.AddComponent<Building>();
        placedata = data;
        _spawnedBuilding.Init(data);
        _spawnedBuilding.transform.rotation = _lastRotation;
//        Debug.Log("Selected part: " + data.name);
        if (data.name == "FloorSign")
        {
            
        }
    }
    private bool cursorAtivo = true;
    private void Update()
    {
        IsRayHittingSomething(_buildModeLayerMask, out RaycastHit hitInfo);
        if(Keyboard.current.jKey.wasPressedThisFrame)
        {
            cursorAtivo = !cursorAtivo; // Inverte o estado do cursor (ativo ou inativo)
            Cursor.visible = cursorAtivo;
            Cursor.lockState = CursorLockMode.None;
            
        }
        if (buildAtivar)
        {
            ChoosePart(data);
        }
        if (_spawnedBuilding && Keyboard.current.escapeKey.wasPressedThisFrame) DeleteObjectPreview();
        if (Keyboard.current.pKey.wasPressedThisFrame) _deleteModeEnabled = !_deleteModeEnabled;
        
        if (_deleteModeEnabled) DeleteModeLogic();
        else BuildModeLogic();
    }

    private void DeleteObjectPreview()
    {
        if (_spawnedBuilding != null)
        {
            Destroy(_spawnedBuilding.gameObject);
            _spawnedBuilding = null;
        }
    }

    public bool IsRayHittingSomething(LayerMask layerMask, out RaycastHit hitInfo)
    {
        var ray = new Ray(_rayOrigin.position, _camera.transform.forward * _rayDistance);
        if (Physics.Raycast(ray, out hitInfo, _rayDistance, layerMask))
        {
            if(hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("terraArada"))
            {
                //Debug.Log("Farming");
                plantio = true;
               // PositionBuildingPreview();
            }
            else
            {
                //PositionBuildingPreviewFarm();
                plantio = false;
            }
            PlayerManager playerStatus = FindObjectOfType<PlayerManager>();
            if (playerStatus != null)
            {
                //print("raycast");
                playerStatus.raycast(hitInfo);
            }
            return true;
        }
        return false;
    }
    public void DisableObjectPreview()
    {
        if (_spawnedBuilding != null)
        {
            Destroy(_spawnedBuilding.gameObject);
            _spawnedBuilding = null;
        }
    }
     
    private void DeleteModeLogic()
    {
        if (IsRayHittingSomething(_deleteModeLayerMask, out RaycastHit hitInfo))
        {
            var detectedBuilding = hitInfo.collider.gameObject.GetComponentInParent<Building>();

            if (detectedBuilding == null) return;

            if (_targetBuilding == null) _targetBuilding = detectedBuilding;

            if (detectedBuilding != _targetBuilding && _targetBuilding.FlaggedForDelete)
            {
                _targetBuilding.RemoveDeleteFlag();
                _targetBuilding = detectedBuilding;
            }

            if (detectedBuilding == _targetBuilding && !_targetBuilding.FlaggedForDelete)
            {
                _targetBuilding.FlagForDelete(_buildingMatNegative);
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Destroy(_targetBuilding.gameObject);
                _targetBuilding = null;
            }
        }
        else
        {
            if (_targetBuilding != null && _targetBuilding.FlaggedForDelete)
            {
                _targetBuilding.RemoveDeleteFlag();
                _targetBuilding = null;
            }
        }
        
    }


    public void BuildModeLogic()
    {
        if (_targetBuilding != null && _targetBuilding.FlaggedForDelete)
        {
            _targetBuilding.RemoveDeleteFlag();
            _targetBuilding = null;
        }

        if (_spawnedBuilding == null) return;
        
        PositionBuildingPreview();
        
        
    }
    public bool pode = false;
    public void PositionBuildingPreview()
    {
        InventoryItemData Item = FindObjectOfType<InventoryItemData>();
        HotbarDisplay hotbarDisplay = FindObjectOfType<HotbarDisplay>();

        _spawnedBuilding.UpdateMaterial(_spawnedBuilding.IsOverlapping ? _buildingMatNegative : _buildingMatPositive);

        if (Keyboard.current.qKey.isPressed)
        {
            _spawnedBuilding.transform.Rotate(0, -_rotateSnapAngle * Time.deltaTime, 0);
            _lastRotation = _spawnedBuilding.transform.rotation;
        }

        if (Keyboard.current.eKey.isPressed)
        {
            // Girar continuamente para a direita (sentido horário)
            _spawnedBuilding.transform.Rotate(0, _rotateSnapAngle * Time.deltaTime, 0);
            _lastRotation = _spawnedBuilding.transform.rotation;
        }
        
        if (IsRayHittingSomething(_buildModeLayerMask, out RaycastHit hitInfo) && !hotbarDisplay.ItemSemente() && pode)
        {
            var gridPosition = WorldGrid.GridPositionFromWorldPoint3D(hitInfo.point, 0.1f);
            // _spawnedBuilding.transform.position = gridPosition;
            _spawnedBuilding.transform.position = hitInfo.point;

               

            if (Mouse.current.leftButton.wasPressedThisFrame && !_spawnedBuilding.IsOverlapping)
            {

                _spawnedBuilding.PlaceBuilding();
                var dataCopy = _spawnedBuilding.AssignedData;
                _spawnedBuilding = null;
                ChoosePart(dataCopy);
                if (dataCopy.name == "Cerca")
                {
                    Debug.Log("Cerca colocada.");

                    // Adicione a posição da cerca à lista
                    fencePositions.Add(hitInfo.point);

                    if (fencePositions.Count >= 4)
                    {
                        InstantiateCubeforFence();
                        DisableObjectPreview();
                        fencePositions.Clear(); // Limpar a lista após a criação do cercamento
                    }
                }
                else
                {
                    DisableObjectPreview();
                }

                hotbarDisplay.ClearSelectedItem();

                DisableObjectPreview();
            }
        }else if (IsRayHittingSomething(FarmLayer, out RaycastHit hit) && hotbarDisplay.ItemSemente())
        {
            var gridPosition = WorldGrid.GridPositionFromWorldPoint3D(hitInfo.point, 0.1f);
            // _spawnedBuilding.transform.position = gridPosition;
            _spawnedBuilding.transform.position = hitInfo.point;

               
                Debug.Log("Farming");
            if (Mouse.current.leftButton.wasPressedThisFrame && !_spawnedBuilding.IsOverlapping)
            {

                _spawnedBuilding.PlaceBuilding();
                var dataCopy = _spawnedBuilding.AssignedData;
                _spawnedBuilding = null;
                ChoosePart(dataCopy);
                hotbarDisplay.ClearSelectedItem();

                DisableObjectPreview();
            }
        }
        else
        {
            _spawnedBuilding.transform.position = hitInfo.point;
            _spawnedBuilding.UpdateMaterial(_buildingMatNegative);
        }

    }

    public void InstantiateCubeforFence()
    {
        if (fencePositions.Count != 4)
        {
            Debug.LogWarning("Não há posições de cerca suficientes para criar o cercamento.");
            return;
        }
    
        Mesh mesh = new Mesh();
        GameObject fence = new GameObject("Fence");
        fence.AddComponent<MeshFilter>().mesh = mesh;
        fence.AddComponent<MeshRenderer>().material.SetFloat("_Cull", 0);
    
        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[] { 0, 1, 2, 0, 2, 3 };
    
        for (int i = 0; i < 4; i++)
        {
            Vector3 edgeVector = fencePositions[(i + 1) % 4] - fencePositions[i];
            vertices[i] = fencePositions[i] + new Vector3(0, 0.01f, 0) + edgeVector * 0.010f;
        }
    
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    
        
        var customFenceScript = fence.AddComponent<TerraArada>();
        customFenceScript.tipoDeTextura = TerraArada.TipoDeTextura.TerraNormal;
        customFenceScript.TexturaEdit(materialTerraArada, materialTerraNormal, materialTerraPronta);
        
        BoxCollider boxCollider = fence.AddComponent<BoxCollider>();
        
        Debug.Log("Custom fence created.");
    }

    private void OnDrawGizmos()
    {
        if (_camera == null || _rayOrigin == null)
            return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(_rayOrigin.position, _camera.transform.forward * _rayDistance);
    }

}
