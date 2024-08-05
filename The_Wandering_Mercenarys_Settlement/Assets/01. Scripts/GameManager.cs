using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameStart = false;
    public bool isOpenShop = false;
    public bool isOpenSetting = false;
    public bool isGameOver = false;

    private void Awake()
    {
        GameScenes.globalGameManager = this;
        isGameStart = true;
        isOpenShop = false;
        isOpenSetting = false;
        isGameOver = false;
    }
}
