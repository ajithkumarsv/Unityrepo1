using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerUp : MonoBehaviour
{

    [SerializeField] float speed = 10;
    public void Spawn()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
        transform.DOScale(0.8f, 0.5f).SetLoops(-1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            collision.GetComponent<Player>().PowerUp(Random.Range(0, 3));
            Destroy(gameObject);
        }
    }
}
