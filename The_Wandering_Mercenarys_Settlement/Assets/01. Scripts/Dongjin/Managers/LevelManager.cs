using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int baseExp = 10;
    [SerializeField] private float growthMultiplier = 0.1f;
    [SerializeField] private int resetInterval = 5;
    [SerializeField] private int resetMultiplier;
    [SerializeField] private int modValue;


    private void Awake()
    {
        GameScenes.globalLevelManager = this;
        
    }

    public void LevelUp(ref PlayerSO so)
    {
        so.Level++;
        resetMultiplier = (so.Level - 1) / resetInterval;
        modValue = (so.Level - 1) % resetInterval;
        so.nextEXP = (int)(baseExp * Mathf.Pow(1 + growthMultiplier, resetMultiplier * resetInterval + modValue));
        Debug.Log("레벨업!");
    }

    public float GetEXP(int enemyLevel, float exp)
    {
        Debug.Log($"경험치를 {enemyLevel * (int)GameScenes.globalPlayerController.playerSO.nextEXP / 100 + exp} 만큼 획득했다!");
        return enemyLevel * (int)GameScenes.globalPlayerController.playerSO.nextEXP / 100 + exp;
    }
}
