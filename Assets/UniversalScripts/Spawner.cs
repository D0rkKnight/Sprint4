using System;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

[RequireComponent(typeof(TimedSpawner))]
[RequireComponent(typeof(MMObjectPooler))]
public class Spawner : MonoBehaviour, MMEventListener<DifficultyProfileEvent> {
    public MMMultipleObjectPooler pooler;
    public int spawnMax = 3;

    // Populate the pool with the enemy prefabs
    private void Start() {
        GameObject[] prefabs = DifficultyManager.Instance.enemyPrefabs;

        for (int i = 0; i < prefabs.Length; i++) {
            MMMultipleObjectPoolerObject pool = new MMMultipleObjectPoolerObject();

            pool.GameObjectToPool = prefabs[i];
            pool.PoolSize = spawnMax;
            pool.PoolCanExpand = false;
            pool.Enabled = false;

            pooler.Pool.Add(pool);
        }

        pooler.FillObjectPool();

        // Set the difficulty to default
        SetDifficulty(DifficultyManager.Instance.difficulty);
    }

    private void Reset() {
        pooler = GetComponent<MMMultipleObjectPooler>();
    }

    private void OnEnable() {
        this.MMEventStartListening<DifficultyProfileEvent>();
    }

    private void OnDisable() {
        this.MMEventStopListening<DifficultyProfileEvent>();
    }

    public void OnMMEvent(DifficultyProfileEvent eventType)
    {
        int diff = eventType.difficulty;
        SetDifficulty(diff);
    }

    private void SetDifficulty(int diff)
    {
        int prevDiff = Math.Max(diff - 1, 0);
        int nextDiff = Math.Min(diff, pooler.Pool.Capacity - 1);

        pooler.Pool[prevDiff].Enabled = false;
        pooler.Pool[nextDiff].Enabled = true;
    }
}