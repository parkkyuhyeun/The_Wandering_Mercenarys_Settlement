using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKey : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameScenes.globalPlayerController.playerSO.curEXP = GameScenes.globalPlayerController.playerSO.nextEXP;
            GameScenes.globalPlayerController.LevelUpManage();

            Debug.Log("강제 레벨업");
        }

        if(Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.N))
        {
            GameScenes.globalWaveManager.MonstersInWaves(GameScenes.globalWaveManager.GetCurWave() + 1);
            Debug.Log("다음 웨이브 강제 시작");
        }
    }
}
