using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints; 
    int waypointIndex = 0;
    EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void setWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    /*This script is helping enemy to choose the
     * desired path to move and also destroying the 
     * gameObject after there is no path to move
     * And also decreasing ememy count after enemy is destroyed */
    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
            /* ------------Decreasing Ememy Count when die--------------------- */
            enemySpawner.totalEnemies--;            
            if (enemySpawner.totalEnemies <= 0)
            {
                StartCoroutine(waitToLoad());
                FindObjectOfType<LoadScean>().loadNextLevel();
            }
            Debug.Log("Enemies left to kill: "+enemySpawner.totalEnemies);
            /* ---------------------------------------------------------------- */
        }
    }

    IEnumerator waitToLoad()
    {
        yield return new WaitForSeconds(2);
        
    }
}
