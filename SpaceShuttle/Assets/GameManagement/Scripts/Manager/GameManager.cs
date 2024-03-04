using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GM
{
    public class GameManager : Singleton<GameManager>
    {
        private static bool isPaused;
        public static bool IsPaused
        {
            get
            {
                return isPaused;
            }
            set
            {
                isPaused = value;
            }

        }



        private GameData gameData;


        public void SaveData()
        {
            if(gameData == null)
            {
                gameData = new GameData();
            }
            
            //gameData.playerScore = 100;
            //gameData.isGameCompleted = false;
            gameData.playerName = "Player";

            // Save game data
            saveManager.Save(gameData);
        }


        public void LoadData()
        {
            if (saveManager)
            {

            }
            else
            {
                Debug.Log("savemanager is null");
            }
            GameData loadedData = saveManager.Load();
            if (loadedData != null)
            {
                gameData = loadedData;
                //Debug.Log("Loaded player score: " + loadedData.playerScore);
                //Debug.Log("Loaded game completed status: " + loadedData.isGameCompleted);
                Debug.Log("Loaded player name: " + loadedData.playerName);
            }
            else
            {
                SaveData();
            }
            SettingsManager.Instance.Load();
        }


        #region gameFlow
        GameplaySource gameplaySource;


        public GameplaySource GameplaySource { get { return gameplaySource; } }

        public void InitGame(Action<GameStartOptions> callback)
        {

            StartCoroutine(InitValues(callback));
            //StartGame(gameStartParameters);
        }

        public IEnumerator InitValues(Action<GameStartOptions> callback)
        {
            yield return null;
            //yield return new WaitUntil(() => (LevelSaveLoadData.Instance != null));
            //LevelData leveldata = LevelSaveLoadData.Instance.load();
            //if (leveldata == null)
            //{
            //    Debug.LogError("LevelData is NUll");
            //    //yield return null;
            //}
            gameplaySource = new GameplaySource();
            GameStartOptions gameStartOptions = new GameStartOptions();

            gameStartOptions.gameUIHandler = GameUIController.Instance.gameuihandler;
            gameStartOptions.totalTime = 180;

            callback?.Invoke(gameStartOptions);
        }
        public void StartGame(GameStartOptions gameStartOptions)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
            Time.timeScale = 1;
            IsPaused = false;
            LockUnlockCursor(true);
            
            GameUIController.Instance.OnLoaded();
            WeatherManager.Instance.ChangeSkyToDefault();
            StartTimer(OnTimerComplete);
            gameplaySource.Begin(gameStartOptions);
        }
        public void StartTimer(Action onTimerCompletecallback)
        {
            StartCoroutine(TimerCoroutine(onTimerCompletecallback));
        }

        IEnumerator TimerCoroutine(Action OnComplete)
        {
            int Count = 3;
            while (Count >= 0)
            {
                Debug.Log("Working");
                if (Count == 0)
                {
                    GameUIController.Instance.gameuihandler.StartTime("GO");
                }
                else
                {
                    GameUIController.Instance.gameuihandler.StartTime(Count.ToString());
                    //timer.StartTime(Count.ToString());
                }

                Count--;
                yield return new WaitForSeconds(1);
            }
            GameUIController.Instance.gameuihandler.DisableTimer();
            OnComplete?.Invoke();
        }
        public void OnTimerComplete()
        {
            InputManager.IsInputEnabled = true;
            FindObjectOfType<EnemySpawner>().StartSpawn();
        }
        public void Retry()
        {
            //StartGame();
            ReloadScene();
        }

        public void ReloadScene()
        {
            LoadingManager.Instance.RetryGame(OnGameSceneLoaded);
        }

        public void LockUnlockCursor(bool islock)
        {

            if (islock)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

        }

        public void PlayGame()
        {
            LockUnlockCursor(true);
            loadingManager.LoadGamePlay(OnGameSceneLoaded);
            
        }

        public void LoadMenu()
        {
            loadingManager.LoadMenu(OnMenuSceneLoaded);
        }

        public void PauseGame()
        {
            IsPaused = true;
            InputManager.IsInputEnabled = false;
            LockUnlockCursor(false);
            Time.timeScale = 0;
        }
        public void ResumeGame()
        {
            isPaused = false;
            InputManager.IsInputEnabled = true;
            LockUnlockCursor(true);
            Time.timeScale = 1;
        }

        public void OnGameOver(GameOverOption gameOverOption)
        {
            InputManager.IsInputEnabled = false;
            LockUnlockCursor(false);
            Time.timeScale = 0;
           
            if(gameData.highScore< gameOverOption.score)
            {
                gameData.highScore = gameOverOption.score;
               
            }
               
            SaveData();
            GameUIController.Instance.ActivateGameoverhandler();
            GameUIController.Instance.GameOver(gameOverOption,gameData.highScore);
        }



        public void OnPreLoaded()
        {
            menuController.OnLoaded();
            LoadData();
        }

        public void OnGameSceneLoaded()
        {
            InitGame(StartGame);
            //StartGame();
        }

        public void OnMenuSceneLoaded()
        {
            MenuController.Instance.OnLoaded();
            Debug.Log("MenuScenes Loaded");
        }

        private void Update()
        {

            if (gameplaySource != null) { gameplaySource.Tick(Time.deltaTime); }

        }

        #endregion
        LoadingManager loadingManager { get { return LoadingManager.Instance; } }
        MenuController menuController { get { return MenuController.Instance; } }
        SaveManager saveManager { get { return SaveManager.Instance; } }
    }
}