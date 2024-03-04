using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PowerUpIndicator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;


    public void Activate(string text)
    {
        TextMeshProUGUI t = Instantiate(this.text,transform);
        t.text = text;
        t.DOColor(Color.clear, 2).OnComplete(() => Destroy(t.gameObject));
    }
    
}
