
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class GameUIController : Singleton<GameUIController>
    {
        Handler currentActiveHandler = null;

        [SerializeField] PauseHandler pausehandler;
        [SerializeField] GameUIHandler gameUIHandler;
        [SerializeField] OptionsHandler optionsHandler;
        [SerializeField] GameOverHandler gameoverHandler;

        public GameUIHandler gameuihandler { get { return gameUIHandler; } }
        public void OnLoaded()
        {
            DeactivateAll();
            ActivateGameHandler();
        }

        public void DeactivateAll()
        {
            pausehandler.DeInit();
            gameUIHandler.DeInit();
            optionsHandler.DeInit();
            gameoverHandler.DeInit();
        }

        public void ActivateHandler(Handler handler)
        {
            if (currentActiveHandler != null) currentActiveHandler.DeInit();
            handler.Init();
            currentActiveHandler = handler;
        }

        public void CrossHair(bool enable)
        {
            gameUIHandler.CrossHair(enable);
        }

        public void ActivatePauseHandler()
        {
            ActivateHandler(pausehandler);
        }

        public void ActivateOptionsHandler()
        {
            ActivateHandler(optionsHandler);
        }

        public void ActivateGameoverhandler()
        {
            ActivateHandler(gameoverHandler);
        }

        public void PauseGame()
        {
            gameManager.PauseGame();
            ActivatePauseHandler();
        }

        public void Resume()
        {
            gameManager.ResumeGame();
            pausehandler.OnResume();
        }

        public void OnMenu()
        {
            gameManager.LoadMenu();
        }
        public void OnRetry()
        {

            gameManager.Retry();
        }

        public void ActivateGameHandler()
        {
            ActivateHandler(gameUIHandler);
        }

        public void GameOver(GameOverOption options,int highscore)
        {
            gameoverHandler.SetScore(options.score, highscore);

        }


        GameManager gameManager { get { return GameManager.Instance; } }


    }
}