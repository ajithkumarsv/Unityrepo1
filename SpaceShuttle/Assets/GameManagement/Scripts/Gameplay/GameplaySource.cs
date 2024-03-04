using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


namespace GM
{
    public struct GameStartOptions
{
    public float totalTime;
    public GameUIHandler gameUIHandler;

}

public struct GameOverOption
{
    public float TimeTaken;
    public string GameOverCause;
    public int score;
}

public class GameplaySource 
{

    protected int score = 0;
    protected bool isGameOver = false;
    GameUIHandler gameUIHandler;

    public virtual void Begin(GameStartOptions startParameters)
    {
            this.gameUIHandler=startParameters.gameUIHandler;
    }
    public virtual void Tick(float delta)
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (!GameManager.IsPaused)
            {
                //GameManager.Instance.PauseGame();
                GameUIController.Instance.PauseGame();
            }
            else
            {
                //GameManager.Instance.ResumeGame();
                GameUIController.Instance.Resume();
            }

        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            if (Cursor.visible)
            {
                GameManager.Instance.LockUnlockCursor(true);

            }
            else
            {
                GameManager.Instance.LockUnlockCursor(false);
            }

        }
    }

        public void SetScore(int score)
        {
            this.score += score;
            gameUIHandler.SetScore(this.score, score);
        }

        public void SetPlayerHealth(int current, int total)
        {
           gameUIHandler.  SetHealth(current, total);
        }

    
    public void PlayerDead()
        {
            
            GameOverOption gameOverOption = new GameOverOption();
            gameOverOption.score = score;
            End(gameOverOption);
        }

       
    public virtual void End(GameOverOption gameOverParameter)
    {
            GameManager.Instance.OnGameOver(gameOverParameter);
    }

    //public virtual void OnHitTarget(int val) { }
}


}
