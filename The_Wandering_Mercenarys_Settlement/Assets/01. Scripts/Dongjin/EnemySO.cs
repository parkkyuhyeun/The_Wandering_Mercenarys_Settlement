using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [Header("코인관련(코인 레벨은 0~4까지)")]
    public int GiveCoin;
    public int CoinLevel;
    [Header("0: 근접, 1: 원거리, 2: 둘다")]
    [Range(0, 1)] public int Series;

    [Header("원거리만 사용")]
    public float bulletLifeTime;
    public float bulletSpeed;
    public ObjectType.WeaponType weaponType;

    [Header("스탯 증가/감소 비율")]
    public float hpIncreaseRate; // 체력 증가 비율
    public float damageIncreaseRate; // 공격력 증가 비율
    public float cooldownDecreaseRate; // 공격 쿨타임 감소 비율
    [Header("원거리 스탯 증가/감소 비율")]
    public float rangeIncreaseRate; // 사정거리 증가 비율
    public float bulletLifeIncreaseRate; // 총알 생존 시간 증가 비율
    public float bulletSpeedIncreaseRate; // 총알 속도 증가 비율

    //저장 변수
    private float _maxHP;
    private float _damage;
    private float _attackCooldown;
    private float _attackDistance;
    private float _bulletLifeTime;
    private float _bulletSpeed;

    public void ResetData()
    {
        MaxHP = _maxHP;
        Damage = _damage;
        AttackCooldown = _attackCooldown;
        AttackDistance = _attackDistance;
        bulletLifeTime = _bulletLifeTime;
        bulletSpeed = _bulletSpeed;
    }

    private void Awake()
    {
        _maxHP = MaxHP;
        _damage = Damage;
        _attackCooldown = AttackCooldown;
        _attackDistance = AttackDistance;
        _bulletLifeTime = bulletLifeTime;
        _bulletSpeed = bulletSpeed;
    }
}
