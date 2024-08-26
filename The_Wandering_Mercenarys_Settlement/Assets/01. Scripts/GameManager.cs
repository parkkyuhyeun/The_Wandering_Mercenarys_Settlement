using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameStart = false;

    private void Awake()
    {
        GameScenes.globalGameManager = this;
       
    }
}
