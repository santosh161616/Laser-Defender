using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config Params
    [Header("Player Movement")]
    [SerializeField] int playerHealth = 300;
    [SerializeField] float moveSpeed = 10f;

    [Header("projectile")]
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject explosionVFX;

    [SerializeField] GameObject laserSecondPrefab; 
    

    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    
    [SerializeField] AudioClip playerDeathSFX;
    [SerializeField] AudioClip playerShootSFX;
    [SerializeField] [Range(0, 1)] float playerShootSFXVloume = 0.7f;
         
    Coroutine firingCoroutine;

    private float minX, maxX;
    private float minY, maxY;

    public bool laserFirstCheck = false;
    public bool laserSecondCheck = false;

    //Mobile touch Input 
    private Rigidbody2D rb;
    private float deltaX, deltaY;

    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetUpMoveBoundries();
    }

    void Update()
    {
        Move();
        MobileMovement();
        Fire();
    }

    
       
        private void MobileMovement()
        {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    break;
                case TouchPhase.Moved:
                    rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    break;
                case TouchPhase.Ended:
                    rb.velocity = Vector2.zero;
                    break;

            }
        }
    } 

    /* Helping player to fire lasers*/
    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinusly());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    /* Helping above script to make sure player 
     * fires continusly */
    IEnumerator FireContinusly()
    {
        while (true) {

            if(laserFirstCheck)
            {
                GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
               // GameObject laser2 = Instantiate(laserPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
               // laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            }
            if(laserSecondCheck)
            {
                GameObject laserSecond = Instantiate(laserSecondPrefab,transform.position, Quaternion.identity) as GameObject;
                laserSecond.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            }
            AudioSource.PlayClipAtPoint(playerShootSFX, Camera.main.transform.position, playerShootSFXVloume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    /* Setting up the boundry so that player don't go out the scean*/
    private void SetUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    /* Simple script using GetAxis to move player*/
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;         
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var moveY = transform.position.y + deltaY;
        var moveX = transform.position.x + deltaX;

        var newPosX = Mathf.Clamp(moveX, minX, maxX);
        var newPosY = Mathf.Clamp(moveY, minY, maxY);
        transform.position = new Vector2(newPosX, newPosY);
       
    }

    /* Player health is being monitore by this script*/
    public int getPlayerHealth()
    {
        return playerHealth;
    }

    /* Checking if player is being hit by powerUp or enemylaser */
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();
        CheckActiveLaser checkActiveLaser = other.gameObject.GetComponent<CheckActiveLaser>();
      //  ActivateShield activateShield = other.gameObject.GetComponent<ActivateShield>();
        Shield shield = other.gameObject.GetComponent<Shield>();

        if(damageDealer)
        {
            playerHit(damageDealer);
            GameObject particleVFX = Instantiate(explosionVFX, transform.position, Quaternion.identity) as GameObject;
            Destroy(particleVFX, 1f);
        }
        else if (powerUp)
        {
            playerHitPowerUp(powerUp);
        } 
        else if (checkActiveLaser)
        {
            ActivateLaser(checkActiveLaser);
        }
        else if(shield)
        {
            shield.getHit();
            FindObjectOfType<ActivateShield>().activateFirstLaser();
        }
        else
        {
            return;
        }          
    }

    /* If player is being hit by enemy or enemy Laser
     * this this helps reduce player health*/
    private void playerHit(DamageDealer damageDealer)
    {
        playerHealth -= damageDealer.getDamage();        
        FindObjectOfType<CameraShake>().ShakeIt();
        damageDealer.hit();
        if(playerHealth <= 0)
        {
            FindObjectOfType<LoadScean>().loadGameOver();
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(playerDeathSFX, Camera.main.transform.position, 0.7f);
            
        }
        
    }

    /* If player is being hit by powerUp then increasing 
     * player health 
     */
    private void playerHitPowerUp(PowerUp powerUp)
    {
        playerHealth += powerUp.getPower();
        powerUp.getHit();
    }

    private void ActivateLaser(CheckActiveLaser checkActiveLaser)
    {
        checkActiveLaser.ActivateLaser();
        checkActiveLaser.getHit();
    }

}
