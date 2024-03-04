using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TimerObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    public void StartTime(string Time)
    {
        transform.localScale = Vector3.one;
        transform.localEulerAngles = new Vector3(0, 0, 90);
        timer.gameObject.SetActive(true);
        timer.text = Time;
        transform.DOScale(1.5f, 0.5f);
        transform.DORotate(Vector3.zero, 0.5f);
    }
    public void DisableTimer()
      
    {
        timer.gameObject.SetActive(false);
        timer.text = "3";
    }

}
