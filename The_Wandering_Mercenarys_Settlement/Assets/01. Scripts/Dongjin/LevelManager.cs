using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{


    private void Awake()
    {
        GameScenes.globalLevelManager = this;
    }

    public float GetEXP()
    {

        return 0.0f;
    }
}
