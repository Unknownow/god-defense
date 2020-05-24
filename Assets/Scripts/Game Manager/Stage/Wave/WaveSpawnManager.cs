using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnManager : MonoBehaviour
{
    public delegate void OnWaveEnds();
    private event OnWaveEnds _onWaveEndsSubscribers;

    private struct WaveDetail
    {
        public int totalEnemiesCount;
        public int spawnedEnemiesCount;
        public int waveIndex;
        public Dictionary<int, List<Enemy>> enemies;
        public List<GameObject> spawnedEnemies;
        public bool isDone;
    }

    private WaveDetail _currentWave;
    private StageTimerManager _timer;
    private List<SpawnerController> _spawnersList;

    private void Awake()
    {
        _timer = gameObject.GetComponent<StageTimerManager>();
        GetSpawnersList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _spawnersList[0].SpawnEnemy(EnemyType.Runner);
        }
    }

    public void StartWave(Wave wave)
    {
        _currentWave.waveIndex = wave.waveIndex;
        _currentWave.totalEnemiesCount = wave.enemies.Length;
        _currentWave.spawnedEnemiesCount = 0;
        _currentWave.isDone = false;

        if (_currentWave.enemies == null)
            _currentWave.enemies = new Dictionary<int, List<Enemy>>();
        else
            _currentWave.enemies.Clear();

        if (_currentWave.spawnedEnemies == null)
            _currentWave.spawnedEnemies = new List<GameObject>();
        else
            _currentWave.spawnedEnemies.Clear();

        foreach (Enemy enemy in wave.enemies)
        {
            if (_currentWave.enemies.ContainsKey(enemy.spawnTime))
                _currentWave.enemies[enemy.spawnTime].Add(enemy);
            else
            {
                List<Enemy> enemies = new List<Enemy>();
                enemies.Add(enemy);
                _currentWave.enemies.Add(enemy.spawnTime, enemies);
            }
        }
        _timer.SubscribeOnWaveTimerIncrease(OnWaveTimeIncrease);
        _timer.StartWaveTimer();
    }

    public void SubscribeOnWaveEnd(OnWaveEnds subscriber)
    {
        _onWaveEndsSubscribers += subscriber;
    }

    public void UnsubscribeOnWaveEnd(OnWaveEnds subscriber)
    {
        _onWaveEndsSubscribers -= subscriber;
    }

    private void OnWaveTimeIncrease(int currentTime)
    {
        if (_currentWave.enemies.ContainsKey(currentTime))
        {
            foreach (Enemy enemy in _currentWave.enemies[currentTime])
            {
                _currentWave.spawnedEnemiesCount += 1;
                GameObject enemyObject = _spawnersList[enemy.laneIndex].SpawnEnemy(enemy.enemyType);
                if (!_currentWave.spawnedEnemies.Contains(enemyObject))
                    _currentWave.spawnedEnemies.Add(enemyObject);
            }
        }
        if (CheckWaveDone())
        {
            EndWave();
        }

    }

    private void EndWave()
    {
        Debug.Log("wave " + _currentWave.waveIndex + " ended");
        _timer.StopWaveTimer();
        _currentWave.isDone = true;
        _timer.UnsubscribeOnWaveTimerIncrease(OnWaveTimeIncrease);
        _onWaveEndsSubscribers?.Invoke();
    }

    private void GetSpawnersList()
    {
        SpawnerController[] spawners = GameObject.FindObjectsOfType<SpawnerController>();
        if (_spawnersList == null)
            _spawnersList = new List<SpawnerController>();
        else
            _spawnersList.Clear();
        foreach (SpawnerController spawner in spawners)
        {
            _spawnersList.Add(spawner);
        }
        _spawnersList.Sort();
    }

    private bool CheckWaveDone()
    {
        if (_currentWave.spawnedEnemiesCount < _currentWave.totalEnemiesCount)
            return false;

        foreach (GameObject enemy in _currentWave.spawnedEnemies)
        {
            if (enemy.activeSelf)
                return false;
        }
        return true;
    }
}
