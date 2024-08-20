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
    public float GiveExp;
    [Header("0: 근접, 1: 원거리, 2: 둘다")]
    [Range(0, 1)] public int Series;

    // 원거리만 사용
    public float bulletLifeTime;
    public float bulletSpeed;
    public ObjectType.WeaponType weaponType;

    [Header("스탯 증가/감소 비율")]
    public float hpIncreaseRate; // 체력 증가 비율
    public float damageIncreaseRate; // 공격력 증가 비율
    public float cooldownDecreaseRate; // 공격 쿨타임 감소 비율
    public float rangeIncreaseRate; // 사정거리 증가 비율
    public float bulletLifeIncreaseRate; // 총알 생존 시간 증가 비율
    public float bulletSpeedIncreaseRate; // 총알 속도 증가 비율
}
