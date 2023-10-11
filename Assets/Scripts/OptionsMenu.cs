using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public float sensi = 5.0f;
    [SerializeField] private Slider slider;
    [SerializeField] private Text sensiTxt;
    [SerializeField] private Dropdown grafico;
    
    public void SetSensitivity(float newSensi){
        sensi = newSensi;
        PlayerPrefs.SetFloat("Sensibility", newSensi);
    }

    public void GetSensibility(){
        if (PlayerPrefs.HasKey("Sensibilidade"))
        {
            sensi = PlayerPrefs.GetFloat("Sensibilidade");
        }
    }

    private void Update()
    {
        slider.value = sensi;
        sensiTxt.text = sensi.ToString();
    }
    
    public void SetQuality(int qualityLevel){
        QualitySettings.SetQualityLevel(qualityLevel);
    }
}
