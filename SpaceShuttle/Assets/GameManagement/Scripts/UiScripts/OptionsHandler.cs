using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

namespace GM
{
    public class OptionsHandler : Handler
    {
        [SerializeField] Slider volumeSilder;
        [SerializeField] Toggle fullscreenToggle;
        [SerializeField] TMP_Dropdown dropdownquality;




        public override void Init()
        {
            base.Init();
            SettingsData settingsData = settingsManager.GetSettingsDaa();
            if (settingsData != null)
            {
                SetQuality(settingsData.quality);
                SetVolume(settingsData.volume);
                SetFullScreen(settingsData.isFullScreen);

            }

        }
        public override void DeInit()
        {
            base.DeInit();
        }

        public void CloseButtonMenu()
        {
            MenuController.Instance.ActivateMenuHandler();
        }

        public void CloseButtonGame()
        {
            GameUIController.Instance.ActivatePauseHandler();

        }

        public void SetQuality(int quality)
        {
            settingsManager.SetQualityLevel(quality);
            dropdownquality.value = quality;
            Debug.Log("quality " + quality);
        }
        public void SetVolume(float volume)
        {
            settingsManager.SetMasterVolume(volume);
            volumeSilder.value = volume;
            Debug.Log("volume " + volume);
        }
        public void SetFullScreen(bool val)
        {
            settingsManager.SetFullscreen(val);
            fullscreenToggle.isOn = val;
            Debug.Log("Value " + val);
        }

        public void Save()
        {
            settingsManager.Save();
        }


        SettingsManager settingsManager { get { return SettingsManager.Instance; } }

    }
}