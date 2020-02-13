using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> WaveConfigs;
    int startingWaveIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        WaveConfig currWave = WaveConfigs[startingWaveIndex];
        StartCoroutine(SpawnWave(currWave));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnSingleAlien(GameObject alienPrefab, WaveConfig waveConfig) {
        var spawnPos = waveConfig.GetWayPoints()[0].transform.position;
        var alien = Instantiate(alienPrefab, spawnPos, Quaternion.identity);
        alien.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
    }

    private IEnumerator SpawnWave(WaveConfig wave) {
        for (int i=0; i<wave.GetNumOfEnemies(); i++) {
            var spawnPos = wave.GetWayPoints()[0].transform.position;
            SpawnSingleAlien(wave.GetEnemyPrefab(), wave);
            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
        }
    }
}
