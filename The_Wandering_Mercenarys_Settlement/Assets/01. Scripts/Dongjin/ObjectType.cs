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
        public MonsterType name; //�̸�
        public int spawnTime; //���� ��Ÿ��
        public int spawnDistance; //������ �� �÷��̾���� �Ÿ�
    }
}
