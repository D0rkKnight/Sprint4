using System;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;

public class DifficultyManager : MMPersistentSingleton<DifficultyManager>, MMEventListener<MMCharacterEvent> {
    
    public int difficulty = 0;
    public bool pinToCharLevel = false;

    public GameObject[] enemyPrefabs;

    private void OnEnable() {
        this.MMEventStartListening<MMCharacterEvent>();
    }

    private void OnDisable() {
        this.MMEventStopListening<MMCharacterEvent>();
    }

    // If pinned to char level, then update difficulty when char levels up
    public void OnMMEvent(MMCharacterEvent eventType) {
        if (eventType.EventType == MMCharacterEventTypes.LevelUp && pinToCharLevel) {
            SetDifficulty(eventType.TargetCharacter.GetComponentInChildren<Experience>().getLevel());
        }
    }

    public void SetDifficulty(int difficulty) {
        this.difficulty = difficulty;
        GameObject enemyPrefab = enemyPrefabs[Math.Clamp(difficulty, 0, enemyPrefabs.Length - 1)];

        // Debug.Log("Difficulty set to " + difficulty);

        // Send and MMEvent for the new difficulty profile
        MMEventManager.TriggerEvent(new DifficultyProfileEvent(difficulty, enemyPrefab));
    }
}

public struct DifficultyProfileEvent {
    public int difficulty;
    public GameObject enemyPrefab;
    public DifficultyProfileEvent(int difficulty, GameObject enemyPrefab) {
        this.difficulty = difficulty;
        this.enemyPrefab = enemyPrefab;
    }
}