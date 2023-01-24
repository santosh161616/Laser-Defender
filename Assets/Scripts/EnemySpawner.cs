using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    public int totalEnemies;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    
    }

    /* This part of code is being used so that all
     * Enemy waves can spawn after a specifi time */
    private IEnumerator SpawnAllWaves()
    {

        for ( int waveIndex = 0; waveIndex < waveConfigs.Count; waveIndex++ )
        {

            var currentWave = waveConfigs[waveIndex];

            if(waveIndex == 0)
            {                
                for(int i = 0; i < waveConfigs.Count; i++)
                {
                    totalEnemies += waveConfigs[i].GetNumberOfEnemies();                                    
                }
                Debug.Log("Total Number of Enemies: "+totalEnemies);
            }                                
            yield return StartCoroutine(spawnAllEnemiesWave(currentWave));
        }
    }

    /* It's the part of above script helpin it
     * to spawn all enemies */
    private IEnumerator spawnAllEnemiesWave(WaveConfig waveConfig)
    {        

        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
            waveConfig.GetWayPoints()[0].transform.position,
            Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().setWaveConfig(waveConfig); 
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        
    }
}
