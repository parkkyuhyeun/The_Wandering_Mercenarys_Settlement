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
                for(int i = 0; i < wave.monsters.Length; i++)
                {
                    monsters[i] = GetMonster(wave.monsters[i]);
                }
            }
        }
        return null;
    }

    private ObjectType.Monster GetMonster(ObjectType.MonsterType monsterType)
    {
        ObjectType.Monster monster = new ObjectType.Monster(monsterType, 0,0,0);
        return monster;
    }
}
