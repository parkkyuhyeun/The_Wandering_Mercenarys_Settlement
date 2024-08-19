using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsSO", menuName = "EnemyStats")]
public class EnemySO : ScriptableObject
{
    public ObjectType.MonsterType monsterType;
    public float MaxHP;
    public float Damage;
    public float AttackCooldown;
    public float AttackDistance;
    [Header("0: 근접, 1: 원거리, 2: 둘다")]
    public int Series;

    //원거리만 사용

}
