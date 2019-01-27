using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;

    public Wave CurrentWave { get; private set; }

    public float Elapsed { get; private set; }

    public int CurrentRespawnEnemyIndex { get; private set; }

    public GameObject[] RespawnedEnemies { get; private set; }

    public SpawnerStatus Status { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
        Status = SpawnerStatus.NotStarted;
    }

    // Update is called once per frame
    void Update()
    {
        Elapsed += Time.deltaTime;

        if (CurrentRespawnEnemyIndex >= CurrentWave.EnemyCount)
        {
            this.enabled = false;
            Status = SpawnerStatus.Generated;
        }
        else if (Elapsed >= CurrentWave.RespawnTimes[CurrentRespawnEnemyIndex])
        {
            RespawnEnemy();
        }
    }

    private void RespawnEnemy()
    {
        var enemies = Enemies.Where(x => x.GetComponent<Enemy>().level <= CurrentWave.MaxLevel && x.GetComponent<Enemy>().level >= CurrentWave.MinLevel);
        var enemyCount = enemies.Count();

        var index = Random.Range(0, enemyCount - 1);

        var selectedEnemy = enemies.ElementAt(index);

        //TODO: Tile position

        var respawned = Instantiate(selectedEnemy, new Vector3(0, 0, -2), Quaternion.identity);
        RespawnedEnemies[CurrentRespawnEnemyIndex++] = respawned;
    }

    public void SetWave(Wave wave)
    {
        CurrentWave = wave;
        CurrentWave.CreateRespawnTimes();
        Elapsed = 0;
        CurrentRespawnEnemyIndex = 0;
        RespawnedEnemies = new GameObject[CurrentWave.EnemyCount];
        this.enabled = true;
        Status = SpawnerStatus.Started;
    }
}
