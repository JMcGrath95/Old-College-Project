using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatisticsController : MonoBehaviour
{
    [SerializeField] private LevelStatistics levelStatistics;

    private float secondsElapsedAtStartOfLevel;
    private float secondsElapsedAtEndOfLevel;

    private void Awake()
    {
        levelStatistics = new LevelStatistics();

        EnemyHealth.EnemyDeathEvent += OnEnemyDeath;


        //GameController.LevelStartedEvent += OnLevelStarted;
        //GameController.LevelEndedEvent += OnLevelEnded;

        ShaneFakeGameController.LevelStartedEvent += OnLevelStarted;
        ShaneFakeGameController.LevelEndedEvent += OnLevelEnded;
    }

    private void OnEnemyDeath(EnemyHealth enemyHealth)
    {
        levelStatistics.EnemiesKilled++;
    }

    private void OnLevelStarted()
    {
        secondsElapsedAtStartOfLevel = Time.time;
    }

    private void OnLevelEnded()
    {
        secondsElapsedAtEndOfLevel = Time.time;
        levelStatistics.secondsToCompleteLevel = secondsElapsedAtEndOfLevel - secondsElapsedAtStartOfLevel;
    }

    private void OnDestroy()
    {
        EnemyHealth.EnemyDeathEvent -= OnEnemyDeath;
        //GameController.LevelStartedEvent -= OnLevelStarted;
        //GameController.LevelEndedEvent -= OnLevelEnded;
    }
}
