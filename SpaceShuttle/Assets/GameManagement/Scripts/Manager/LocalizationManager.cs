using System;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class LocalizationManager : Singleton<LocalizationManager>
    {

        public event Action OnLanguageChange;

        private Dictionary<string, Dictionary<string, string>> localizedText;
        private string currentLanguage = "en"; // Default language

        private void Awake()
        {

            LoadLocalizedText();
        }

        private void LoadLocalizedText()
        {
            localizedText = new Dictionary<string, Dictionary<string, string>>();

            // Load all language files (e.g., en.json, fr.json, etc.)
            // You can replace this with your own method of loading localization files
            TextAsset[] localizationFiles = Resources.LoadAll<TextAsset>("Localization");
            foreach (TextAsset file in localizationFiles)
            {
                Dictionary<string, string> language = new Dictionary<string, string>();
                LocalizationData data = JsonUtility.FromJson<LocalizationData>(file.text);

                foreach (LocalizationItem item in data.items)
                {
                    language[item.key] = item.value;
                }

                localizedText[data.language] = language;
            }
        }

        public void SetLanguage(string languageCode)
        {
            if (localizedText.ContainsKey(languageCode))
            {
                currentLanguage = languageCode;
                OnLanguageChange?.Invoke();
            }
            else
            {
                Debug.LogWarning("Language " + languageCode + " not found.");
            }
        }

        public string GetLocalizedValue(string key)
        {
            if (localizedText.ContainsKey(currentLanguage))
            {
                if (localizedText[currentLanguage].ContainsKey(key))
                {
                    return localizedText[currentLanguage][key];
                }
                else
                {
                    Debug.LogWarning("Key " + key + " not found in language " + currentLanguage);
                }
            }
            else
            {
                Debug.LogWarning("Language " + currentLanguage + " not found.");
            }

            return "";
        }
    }

    [Serializable]
    public class LocalizationData
    {
        public string language;
        public LocalizationItem[] items;
    }

    [Serializable]
    public class LocalizationItem
    {
        public string key;
        public string value;
    }
}