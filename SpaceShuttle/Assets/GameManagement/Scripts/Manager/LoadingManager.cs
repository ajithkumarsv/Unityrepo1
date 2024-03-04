using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GM
{
    public class LoadingManager : Singleton<LoadingManager>
    {
        [SerializeField]
        List<string> LoadedScenes;
        [SerializeField]
        GameObject LoadingScreen;


        public void LoadGamePlay(Action callback)
        {
            string[] loadingscenes = { "GameUI", "Game" };
            string[] removingscenes = { "MenuUI" };
            LoadScenes(loadingscenes, removingscenes, callback);
            

        }
        public void RetryGame(Action callback)
        {
            string[] reloadingscenes = { "Game" };
            ReloadScenes(reloadingscenes, callback);

            
            
        }
        public void ReloadScenes(string[] scenes, Action onCompleted)
        {
            StartCoroutine(ReloadSceneCoroutine(scenes, onCompleted));
        }
        public IEnumerator ReloadSceneCoroutine(string[] scenes, Action onCompleted)
        {
            GameObject go = Instantiate(LoadingScreen);
            foreach (string removingscene in scenes)
            {
                AsyncOperation async = SceneManager.UnloadSceneAsync(removingscene);
                yield return new WaitUntil(() => async.isDone);
                async = Resources.UnloadUnusedAssets();
                yield return new WaitUntil(() => async.isDone);
                //LoadedScenes.Add(scene);
            }
            foreach (string scene in scenes)
            {
                AsyncOperation async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                yield return new WaitUntil(() => async.isDone);
                LoadedScenes.Add(scene);
            }

            //yield return new WaitForSecondsRealtime(1);
            Destroy(go);
            onCompleted?.Invoke();
        }

        public void LoadMenu(Action callBack)
        {
            string[] removingscenes = { "Game", "GameUI" };
            string[] loadingscenes = { "MenuUI" };
            LoadScenes(loadingscenes, removingscenes, callBack);
        }

        public void LoadScenes(string[] scenes, string[] removingscene, Action onCompleted)
        {
            StartCoroutine(LoadSceneCoroutine(scenes, removingscene, onCompleted));
        }
        public IEnumerator LoadSceneCoroutine(string[] scenes, string[] removingscenes, Action onCompleted)
        {
            GameObject go = Instantiate(LoadingScreen);
            foreach (string removingscene in removingscenes)
            {
                AsyncOperation async = SceneManager.UnloadSceneAsync(removingscene);
                yield return new WaitUntil(() => async.isDone);
                async = Resources.UnloadUnusedAssets();
                yield return new WaitUntil(() => async.isDone);
                //LoadedScenes.Add(scene);
            }
            foreach (string scene in scenes)
            {
                AsyncOperation async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                yield return new WaitUntil(() => async.isDone);
                LoadedScenes.Add(scene);
            }

            //yield return new WaitForSecondsRealtime(1);
            Destroy(go);
            onCompleted?.Invoke();
        }
        public void LoadScene(string scene, Action onCompleted)
        {
            StartCoroutine(LoadSceneCoroutine(scene, onCompleted));
        }

        public IEnumerator LoadSceneCoroutine(string scene, Action onCompleted)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            yield return new WaitUntil(() => async.isDone);
            LoadedScenes.Add(scene);
            onCompleted?.Invoke();
        }

        public void RemoveScene(string scene, Action onCompleted)
        {
            StartCoroutine(RemoveSceneCoroutine(scene, onCompleted));
        }

        public IEnumerator RemoveSceneCoroutine(string scene, Action onCompleted)
        {
            AsyncOperation async = SceneManager.UnloadSceneAsync(scene);

            yield return new WaitUntil(() => async.isDone);
            async = Resources.UnloadUnusedAssets();
            yield return new WaitUntil(() => async.isDone);
            onCompleted?.Invoke();
        }
        public void RemoveScenes(string[] scenes, Action onCompleted)
        {
            StartCoroutine(RemoveSceneCoroutine(scenes, onCompleted));
        }
        public IEnumerator RemoveSceneCoroutine(string[] scenes, Action onCompleted)
        {
            foreach (string scene in scenes)
            {
                AsyncOperation async = SceneManager.UnloadSceneAsync(scene);
                yield return new WaitUntil(() => async.isDone);
                async = Resources.UnloadUnusedAssets();
                yield return new WaitUntil(() => async.isDone);
                LoadedScenes.Add(scene);
            }
            onCompleted?.Invoke();
        }
    }
}