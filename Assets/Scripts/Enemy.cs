using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 10;
    
    [Header("Enemy Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyBullet;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float enemyBulletSpeed = 10f;
    [SerializeField] float enemyBulletPeriod = 0.1f;
    
    [Header("Audio SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;
    [SerializeField] AudioClip enemyShootSFX;
    [SerializeField] [Range(0, 1)] float enemyShootSFXVolume = 0.7f;

    EnemyPower enemyPower;
    EnemySpawner enemySpawner;
     
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        enemyPower = FindObjectOfType<EnemyPower>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    //Enemy is randomly shooting using this script after checking 
    //the random time after each bullet. 
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter < 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    //This script is responsible to shoot enemy.
    private void Fire()
    {
        
            GameObject laser = Instantiate(enemyBullet, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(enemyShootSFX, Camera.main.transform.position, enemyShootSFXVolume);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyBulletSpeed);         
         
    }

    //Enemy is checking if he is being hit by bullet or not.
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer)
        {
            return;
        }
        processHit(damageDealer);
        GameObject particleVFX = Instantiate(explosionVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(particleVFX, 1f);

    }

    /*After enemy is being hit destroying the gameObject and also reducing power if enemy has
     * more power, Also helping player score to add scores after enemy is being destroyed*/
    private void processHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage();
        damageDealer.hit();
        
        if (health <= 0)
        {
            /* ------------Decreasing Ememy Count when die--------------------- */
            enemySpawner.totalEnemies--;
            if (enemySpawner.totalEnemies <= 0)
            {
                FindObjectOfType<LoadScean>().loadNextLevel();
            }
            Debug.Log("Enemies left to kill: " + enemySpawner.totalEnemies);
            /* --------------------------------------------------------------- */
            if (enemyPower)
            {
                enemyPower.PowerInstantiate();
            }
            Destroy(gameObject);
            FindObjectOfType<GameSession>().addScore(scoreValue);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        }
    }    
}
