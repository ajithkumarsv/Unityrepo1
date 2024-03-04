using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GM
{
    public class GameOverHandler : Handler
    {
        [SerializeField] TextMeshProUGUI highscore;
        [SerializeField] TextMeshProUGUI score;
        public override void Init()
        {
            base.Init();


        }
        public override void DeInit()
        {
            base.DeInit();
        }

        public void SetScore(int score, int highScore)
        {
            highscore.text =highScore.ToString("000");
            this.score.text =score.ToString("000");
        }

        public void Menu()
        {
            GameUIController.Instance.OnMenu();
        }

        public void Retry()
        {
            GameUIController.Instance.OnRetry();
        }

    }
}