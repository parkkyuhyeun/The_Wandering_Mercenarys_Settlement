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
}
