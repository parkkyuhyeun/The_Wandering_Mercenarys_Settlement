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
        public MonsterType name; //�̸�
        public int spawnTime; //���� ��Ÿ��
        public int spawnDistance; //������ �� �÷��̾���� �Ÿ�
        public int spawnCount; //�����Ǵ� ����

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
