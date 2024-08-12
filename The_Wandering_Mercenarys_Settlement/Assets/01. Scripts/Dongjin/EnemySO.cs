using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsSO", menuName = "EnemyStats")]
public class EnemySO : ScriptableObject
{
    public ObjectType.MonsterType monsterType;
    public float MaxHP;
    public float Damage;
}
