using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectType : MonoBehaviour
{
    public enum MonsterType
    {
        empty, Goblin, 
    }

    public enum ObstacleType
    {

    }

    public struct Monster
    {
        public MonsterType name; //이름
        public int spawnTime; //스폰 쿨타임
        public int spawnDistance; //스폰될 때 플레이어와의 거리
    }
}
