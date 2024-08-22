using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="StatsSO", menuName ="PlayerStats")]
public class PlayerSO : ScriptableObject
{
    public float MaxHP;
    public float Damage;
    public int Level;
    public float curEXP;
    public float nextEXP;

    public float AttackCooldown;
    public float damageIncreaseRate;
    public float hpIncreaseRate;
    public float cooldownDecreaseRate;

    private float _maxHP;
    private float _damage;
    private int _level;
    private float _curEXP;
    private float _nextEXP;

    private float _attackCooldown;
    private float _damageIncreaseRate;
    private float _hpIncreaseRate;
    private float _cooldownDecreaseRate;

    public void ResetData()
    {
        MaxHP = _maxHP;
        Damage = _damage;
        Level = _level;
        curEXP = _curEXP;
        nextEXP = _nextEXP;
        AttackCooldown = _attackCooldown;
        damageIncreaseRate = _damageIncreaseRate;
        hpIncreaseRate = _hpIncreaseRate;
        cooldownDecreaseRate = _cooldownDecreaseRate;
    }

    private void OnEnable()
    {
        _maxHP = MaxHP;
        _damage = Damage;
        _level = Level;
        _curEXP = curEXP;
        _nextEXP = nextEXP;
        _attackCooldown = AttackCooldown;
        _damageIncreaseRate = damageIncreaseRate;
        _hpIncreaseRate = hpIncreaseRate;
        _cooldownDecreaseRate = cooldownDecreaseRate;
    }

    
}
