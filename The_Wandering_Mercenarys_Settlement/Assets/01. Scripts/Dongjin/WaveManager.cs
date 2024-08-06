using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WavePackage
{
    public int waveNumber = 0;
    public ObjectType.MonsterType[] monsters;

    public WavePackage(int waveNumber, ObjectType.MonsterType[] monsters)
    {
        this.waveNumber = waveNumber;
        this.monsters = monsters;
    }
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<WavePackage> waveSetting = new List<WavePackage>();

    private void Awake()
    {
        GameScenes.globalWaveManager = this;
    }

    public ObjectType.Monster[] MonstersInWaves(int waveCnt)
    {
        ObjectType.Monster[] monsters = null;
        foreach (WavePackage wave in waveSetting)
        {
            if(wave.waveNumber == waveCnt)
            {
                monsters = new ObjectType.Monster[wave.monsters.Length];
                for (int i = 0; i < wave.monsters.Length; i++)
                {
                    monsters[i] = GetMonster(wave.monsters[i]);
                }
            }
        }
        if (monsters == null) Debug.Log("왜 몬스터가 널이냐??");
        return monsters;
    }

    private ObjectType.Monster GetMonster(ObjectType.MonsterType monsterType)
    {
        if (GameScenes.globalTimer == null || GameScenes.globalTimer.monsters == null)
        {
            Debug.LogError("globalTimer or monsters is null!");
            return null;
        }
        MonsterEntry curMonster = null;
        foreach(var monsterEntry in GameScenes.globalTimer.monsters)
        {
            if(monsterEntry.type == monsterType)
            {
                curMonster = monsterEntry;
            }
        }
        var monster = new ObjectType.Monster(curMonster.type, curMonster.spawnTime, curMonster.spawnDistance, curMonster.spawnCount);
        return monster;
    }
}
