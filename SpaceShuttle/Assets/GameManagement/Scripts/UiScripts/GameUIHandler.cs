using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace GM
{
    public class GameUIHandler : Handler
    {
        GameUIController uiController { get { return GameUIController.Instance; } }

        public GameObject sniperscope;
        //[SerializeField] TextMeshProUGUI timetext;
        [SerializeField] TextMeshProUGUI scoretext;
        [SerializeField] GameObject ActivateObject;
        [SerializeField] TextMeshProUGUI hitscore;


        [SerializeField] TimerObject timer;
        [SerializeField] Image healthImage;
        [SerializeField] PowerUpIndicator idiator;
        private void Start()
        {

        }

        public override void Init()
        {
            base.Init();
            //timetext.text = "00";
            scoretext.text = "000";
            ActivateObject.SetActive(false);
            healthImage.DOFillAmount(1, 1);
        }

        public override void DeInit()
        {
            base.DeInit();
        }


        //public void StartTimer(Action  onTimerCompletecallback)
        //{
        //    StartCoroutine(TimerCoroutine(onTimerCompletecallback));
        //}

        //IEnumerator TimerCoroutine(Action OnComplete)
        //{
        //    int Count = 3;
        //    while(Count >= 0)
        //    {
        //        Debug.Log("Working");
        //        if(Count==0)
        //        {
        //            timer.StartTime("GO");
        //        }
        //        else
        //        {
        //            timer.StartTime(Count.ToString());
        //        }
                
        //        Count--;
        //        yield return new WaitForSeconds(1);
        //    }
        //    timer.DisableTimer();
        //    OnComplete?.Invoke();
        //}
        public void StartTime(string val)
        {
            timer.StartTime(val);
        }
        public void SetPowerUpText(string Text)
        {
            idiator.Activate(Text);
        }
        public void DisableTimer()
        {
            timer.DisableTimer();
        }
        public void CrossHair(bool enable)
        {
            sniperscope.SetActive(enable);
        }

        public void EnableSniperScope(bool val)
        {
            sniperscope.SetActive(val);
        }
        public void ActivatePausehandler()
        {
            uiController.ActivatePauseHandler();
            GameManager.Instance.PauseGame();

        }
        //public void SetTime(float Time)
        //{
        //    timetext.text = Time.ToString("00.00");
        //}
        public void SetScore(float score,float currentscore)
        {
            scoretext.text = score.ToString("000");
            scoretext.transform.DOScale(1.2f, 0.3f).OnComplete(() =>
            scoretext.transform.DOScale(1.0f, 0.3f)
            );
            SetHitScore(currentscore);
            hitscore.transform.DOScale(1.2f, 0.3f).OnComplete(() =>
            hitscore.transform.DOScale(1.0f, 0.3f)
            );
        }
        public void SetHitScore(float score)
        {
            ActivateObject.SetActive(true);
            hitscore.text = $"+{score}";
            StartCoroutine(StopScore());
        }

        public void SetHealth(float current, float max)
        {
            //healthImage.fillAmount=current/max;
            healthImage.DOFillAmount(current / max,0.3f);
            //healthImage.DOColor()
            healthImage.transform.parent.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.2f).OnComplete(() => healthImage.transform.parent.DOScale(Vector3.one, 0.2f));
        }

        IEnumerator StopScore()
        {
            yield return new WaitForSecondsRealtime(2);
            ActivateObject.SetActive(false);
        }

    }
}
