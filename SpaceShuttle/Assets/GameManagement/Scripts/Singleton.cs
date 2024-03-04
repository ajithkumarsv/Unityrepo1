using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<T>();
                return _instance;

            }

        }


    }
}