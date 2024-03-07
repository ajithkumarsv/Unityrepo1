using GM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour {


    private int maxHealth = 500;
    // configuration parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;

    [SerializeField] int health = 500;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [SerializeField] GameObject Shield;
    [SerializeField] GameObject DeadParticles;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    float fireTime=0.2f;

    bool isDead = false;
    bool isImmutable = false;
    // Use this for initialization
    void Start () {
        health = maxHealth;
        SetUpMoveBoundaries();
        fireTime =projectileFiringPeriod;
        //PowerUp(1);
    }
 
    // Update is called once per frame
    void Update () {

        if(!InputManager.IsInputEnabled) return;
        Move();
        Fire();
       

    }

    public void PowerUp(int id)
    {
        switch (id)
        {
            case 0:
                Debug.Log("Health");

                GameUIController.Instance.gameuihandler.SetPowerUpText("Health Pickup");
                health += 100;
                health = Mathf.Clamp(health, 0, maxHealth);
                GameManager.Instance.GameplaySource.SetPlayerHealth(health, maxHealth);
                SpriteRenderer r = GetComponent<SpriteRenderer>();
                transform.DOScale(0.8f, 0.4f).OnComplete(() => transform.DOScale(1, 0.4f));
                break;
            case 1:
                Debug.Log("FIRE");
                GameUIController.Instance.gameuihandler.SetPowerUpText("Fire Power Pickup");
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                projectileFiringPeriod = (projectileFiringPeriod / 2);
                sr.DOColor(Color.yellow,0.2f).OnComplete(()=> { 
                    sr.color = Color.white;
                    //projectileFiringPeriod = (projectileFiringPeriod * 2);
                }).SetLoops(20).OnComplete(() => {
                    sr.color = Color.white;
                    projectileFiringPeriod = (projectileFiringPeriod * 2);
                });

                break;
            case 2:
                Debug.Log("IMMUTABLE");
                GameUIController.Instance.gameuihandler.SetPowerUpText("Invincible Pickup");
                isImmutable = true;
                Shield.SetActive(true);
                Shield.transform.DOScale(1.5f, 0.3f).SetLoops(10).OnComplete(() => { 
                
                    isImmutable=false;
                    Shield.SetActive(false);
                });
               

                break;
            default:
                break;
        }
    }

    //public IEnumerator StopPowerup()
    //{




    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if(isDead || isImmutable) return;
        
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        GameManager.Instance.GameplaySource.SetPlayerHealth (health,maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        isDead = true;
        //FindObjectOfType<Level>().LoadGameOver();
        //GameManager.Instance.GameplaySource.PlayerDead();
        this.enabled =false;
        GameObject deadparticleset=Instantiate(DeadParticles,transform.position, transform.rotation);
        StartCoroutine(Stop());
        
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);   
    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        GameManager.Instance.GameplaySource.PlayerDead();
    }

    public int GetHealth()
    {
        return health;
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1"))
        {
            fireTime-=Time.deltaTime;
            if (fireTime<=0)
            {
                fireTime = projectileFiringPeriod;
                GameObject laser = Instantiate(
                   laserPrefab,
                   transform.position,
                   Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            }
            //firingCoroutine = StartCoroutine(FireContinuously());
        }
        //if (Input.GetButtonUp("Fire1"))
        //{
        //    StopCoroutine(firingCoroutine);
        //}
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                    laserPrefab,
                    transform.position,
                    Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }


    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
