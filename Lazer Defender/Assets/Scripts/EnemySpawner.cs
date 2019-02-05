using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // configuration parameters
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIdx = startingWave; waveIdx < waveConfigs.Count; ++waveIdx)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[waveIdx]));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        var timeBetweenSpawns = waveConfig.GetTimeBetweenSpawns();
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); ++enemyCount)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
