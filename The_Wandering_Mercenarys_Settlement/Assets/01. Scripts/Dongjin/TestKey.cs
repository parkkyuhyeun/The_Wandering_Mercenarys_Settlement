using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKey : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameScenes.globalLevelManager.LevelUp(ref GameScenes.globalPlayerController.playerSO);
            GameScenes.globalPlayerController.PlayerStatSetting();
        }
    }
}
