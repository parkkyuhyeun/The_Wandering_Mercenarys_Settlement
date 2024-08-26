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
    }
}
