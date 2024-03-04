using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class SettingsManager : Singleton<SettingsManager>
    {

        SettingsData settingsData;


        public void SetQualityLevel(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            settingsData.quality = qualityIndex;
        }

        public void SetFullscreen(bool isFullscreen)
        {
            //Screen.fullScreenMode = screenMode;
            Screen.fullScreen = isFullscreen;
            settingsData.isFullScreen = isFullscreen;
        }

        public void SetResolution(int width, int height, bool isFullscreen)
        {
            Screen.SetResolution(width, height, isFullscreen);
        }

        public void SetMasterVolume(float volume)
        {
            AudioManager.Instance.SetMasterVolume(volume);
            settingsData.volume = volume;
        }

        public void Save()
        {
            SaveManager.Instance.SaveSettings(settingsData);
        }

        public void Load()
        {
            settingsData = SaveManager.Instance.LoadSettings();


            Debug.Log("settings" + settingsData.quality);
            Debug.Log("settings" + settingsData.isFullScreen);
            Debug.Log("settings" + settingsData.volume);

            SetQualityLevel(settingsData.quality);
            SetFullscreen(settingsData.isFullScreen);
            SetMasterVolume(settingsData.volume);
        }
        public SettingsData GetSettingsDaa()
        {
            return settingsData;
        }
    }
}