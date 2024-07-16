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

    public class Monster
    {
        public MonsterType name; //이름
        public int spawnTime; //스폰 쿨타임
        public int spawnDistance; //스폰될 때 플레이어와의 거리
        public int spawnCount; //스폰되는 수량

        public Monster()
        {
        }

        public Monster(MonsterType name, int spawnTime, int spawnDistance, int spawnCount)
        {
            this.name = name;
            this.spawnTime = spawnTime;
            this.spawnDistance = spawnDistance;
            this.spawnCount = spawnCount;
        }
    }
}
