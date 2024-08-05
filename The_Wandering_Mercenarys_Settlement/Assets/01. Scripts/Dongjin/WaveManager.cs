using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WavePackage
{
    public int waveNumber = 0;
    public ObjectType.MonsterType monster;

    public WavePackage(int waveNumber, ObjectType.MonsterType monster)
    {
        this.waveNumber = waveNumber;
        this.monster = monster;
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

        return null;
    }
}
