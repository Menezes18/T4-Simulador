using System;
using System.Collections;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    public static UIController instancia;
    [SerializeField] private ShopKeeperDisplay _shopKeeperDisplay;
    [SerializeField] private TMP_Text notificationText;
    private float timeNotification = 2f;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instancia = this;
            
        }
        _shopKeeperDisplay.gameObject.SetActive(false);
    }

    public void ShowNotification(string text)
    {
        StartCoroutine(ShowNotificationIenu(text));
    }

    public IEnumerator ShowNotificationIenu(string text)
    {
        notificationText.text = text;
        notificationText.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeNotification);
        
        notificationText.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        ShopKeeper.OnShopWindowRequested += DisplayShopWindow;
        
    }

    private void OnDisable()
    {
        ShopKeeper.OnShopWindowRequested -= DisplayShopWindow;
        
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            _shopKeeperDisplay.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        
    }

    private void DisplayShopWindow(ShopSystem shopSystem, PlayerInventoryHolder playerInventory)
    {
        
        _shopKeeperDisplay.gameObject.SetActive(true);
        _shopKeeperDisplay.DisplayShopWindow(shopSystem, playerInventory);
    }
}
