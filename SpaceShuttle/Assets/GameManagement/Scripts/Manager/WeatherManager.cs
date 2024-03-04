using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GM
{
    public class WeatherManager : Singleton<WeatherManager>
    {
        public enum WeatherType { Sunny, Cloudy, Rainy }

        public WeatherType currentWeather;
        public Material[] skyMaterials;
        public Light[] directionalLights;

        private int weatherIndex;

        void Start()
        {
            // Set initial weather
            ChangeWeather(currentWeather);
        }

        public void ChangeWeather(WeatherType weatherType)
        {
            switch (weatherType)
            {
                case WeatherType.Sunny:
                    weatherIndex = 0;
                    break;
                case WeatherType.Cloudy:
                    weatherIndex = 1;
                    break;
                case WeatherType.Rainy:
                    weatherIndex = 2;
                    break;
            }


            RenderSettings.skybox = skyMaterials[weatherIndex];
            //RenderSettings.fog = true;

            //for (int i = 0; i < directionalLights.Length; i++)
            //{
            //    directionalLights[i].gameObject.SetActive(i == weatherIndex);
            //}
        }


        public void ChangeSkyToDefault()
        {
            ChangeWeather(WeatherType.Cloudy);
        }
    }
}