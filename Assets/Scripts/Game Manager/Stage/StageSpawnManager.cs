using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageSpawnManager : MonoBehaviour
{
    public delegate void OnWaveDone();
    public delegate void OnStageDone();
    private struct CurrentWave
    {
        public int currentWaveSpawnedEnemiesCount;
        public int currentWaveEnemiesCount;
        public int currentWaveIndex;
        public Dictionary<int, List<Enemy>> currentWaveEnemies;
    }
    private List<SpawnerController> _currentSpawnersList;
    private Stage _currentStage;
    private CurrentWave _currentWave;
    public int WavesCount
    {
        get
        {
            return this._currentStage.waves.Length;
        }
    }
    public int CurrentWaveIndex
    {
        get
        {
            return _currentWave.currentWaveIndex;
        }
    }

    private StageTimerManager _timer;

    private void Awake()
    {
        _timer = gameObject.GetComponent<StageTimerManager>();
    }

    public void LoadStageSpawnDetail(int stageIndex)
    {
        GetStagePattern(stageIndex);
        GetSpawnerPointsList();
        _timer.SubscribeOnWaveTimerIncrease(OnWaveTimeIncrease);
    }

    public void StartWave(int waveIndex)
    {
        _currentWave.currentWaveIndex = waveIndex;
        _currentWave.currentWaveEnemiesCount = 0;
        _currentWave.currentWaveSpawnedEnemiesCount = 0;

        if (_currentWave.currentWaveEnemies == null)
            _currentWave.currentWaveEnemies = new Dictionary<int, List<Enemy>>();
        else
            _currentWave.currentWaveEnemies.Clear();

        foreach (Enemy enemy in _currentStage.waves[waveIndex].enemies)
        {
            if (_currentWave.currentWaveEnemies.ContainsKey(enemy.spawnTime))
                _currentWave.currentWaveEnemies[enemy.spawnTime].Add(enemy);
            else
            {
                List<Enemy> enemies = new List<Enemy>();
                enemies.Add(enemy);
                _currentWave.currentWaveEnemies.Add(enemy.spawnTime, enemies);
            }
        }
        _timer.StartWaveTimer();
    }

    private void GetSpawnerPointsList()
    {
        SpawnerController[] spawners = GameObject.FindObjectsOfType<SpawnerController>();
        if (_currentSpawnersList == null)
            _currentSpawnersList = new List<SpawnerController>();
        else
            _currentSpawnersList.Clear();
        foreach (SpawnerController spawner in spawners)
        {
            _currentSpawnersList.Add(spawner);
        }
        _currentSpawnersList.Sort();
    }

    private void GetStagePattern(int stageIndex)
    {
        _currentStage = Utils.ReadJsonFile<Stage>("JSON/Stages/Stage" + stageIndex);
        foreach (Wave wave in _currentStage.waves)
        {
            Array.Sort(wave.enemies);
        }
    }

    private void OnWaveTimeIncrease(int currentTime, out bool isDone)
    {
        if (_currentWave.currentWaveEnemies.ContainsKey(currentTime))
        {
            foreach (Enemy enemy in _currentWave.currentWaveEnemies[currentTime])
            {
                _currentSpawnersList[enemy.laneIndex].SpawnEnemy(enemy.enemyType);
                _currentWave.currentWaveEnemiesCount += 1;
            }
            if (_currentWave.currentWaveEnemiesCount >= _currentStage.waves[_currentWave.currentWaveIndex].enemies.Length)
            {
                isDone = true;
                return;
            }
        }
        isDone = false;
    }
}
