using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private TowerHitPointManager _towerHitPoint;
    private StageSpawnManager _stageSpawn;
    private bool _isGameOver;
    public bool IsGameOver
    {
        get
        {
            return this._isGameOver;
        }
    }

    void Start()
    {
        _towerHitPoint.SubscribeOnTowerDestroy(OnTowerDestroyed);
        _stageSpawn = gameObject.GetComponent<StageSpawnManager>();
        _isGameOver = true;
    }

    private void StartStage(int stageIndex)
    {
        _stageSpawn.LoadStageDetail(stageIndex);
        _stageSpawn.StartStage();
    }

    private void OnTowerDestroyed()
    {
        Debug.Log("LOSE!");
        _isGameOver = true;
    }
}
