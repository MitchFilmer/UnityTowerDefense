using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Spawner data that contains info on waves; how many of each type of unit in a wave, how many waves, interval between waves for the duration that they are active
/// and the times (in seconds) at which point we step up to the next wave.
/// For instance if we want early, mid, and late game waves to consist of different unit makeups, we can make 3 Waves. Each wave can specify the number and type of units in each wave that spawns,
/// also the delay between wave spawns, and we can say that the mid game waves will start at 180 seconds into the game, and the late game waves will start at 300 seconds into the game. 
/// (by default we assume that the first wave will start at 0 seconds)
/// </summary>
public class SpawnerWaveDefinition : ItemDefinition
{
    [System.Serializable]
    public struct SpawnerWaveData
    {
        public int count;
        public UnitItemDefinition unit;
    }

    // Because Unity won't serialize a List of Lists in the Editor.
    [System.Serializable]
    public class SpawnerWaveDataList
    {
        public List<SpawnerWaveData> waveData;
        public int waveSpawnDelay;
    }

    public List<SpawnerWaveDataList> Waves = new List<SpawnerWaveDataList>();
    public List<int> waveIncrementTimes = new List<int>(3);
}
