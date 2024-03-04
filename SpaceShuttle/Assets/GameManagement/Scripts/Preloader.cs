using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GM
{
    public class Preloader : MonoBehaviour
    {
        public string[] levelNames;
        public string preloaderSceneName;
        GameObject preLoadingCanvas;


        public IEnumerator Start()
        {
            foreach (string s in levelNames)
            {
                AsyncOperation async = SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive);

                while (!async.isDone)
                {
                    yield return null;
                }
            }

            DontDestroyOnLoad(gameObject);
            //yield return new WaitForSeconds(3);
            LoadingManager.Instance?.RemoveScene("PreLoader", OnCompleted);

        }



        public void OnCompleted()
        {

            GameManager.Instance.OnPreLoaded();
            Destroy(gameObject);

        }



    }
}