using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_VolumeSetting : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI txtVolumeValueDisplay;
    [SerializeField] private Slider volumeSlider;

    private void Awake()
    {
        
    }
    private void Start()
    {
        AudioListener.volume = volumeSlider.value / 100;
    }



    //Volume Slider.
    public void OnVolumeSliderValueChanged()
    {
        txtVolumeValueDisplay.text = volumeSlider.value.ToString();
        AudioListener.volume = volumeSlider.value / 100;
    }

    public void SaveVolumeSetting()
    {
        Settings.Volume = volumeSlider.value;
    }
}
