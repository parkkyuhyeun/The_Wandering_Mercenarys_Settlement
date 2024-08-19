using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
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
    [Range(0, 1)] public int Series;

    //원거리만 사용
    public float bulletLifeTime;
    public float bulletSpeed;
    public ObjectType.WeaponType weaponType;
}
