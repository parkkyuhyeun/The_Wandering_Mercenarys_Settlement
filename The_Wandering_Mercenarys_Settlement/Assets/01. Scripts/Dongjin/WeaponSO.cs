using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsSO", menuName = "WeaponStats")]
public class WeaponSO : ScriptableObject
{
    public ObjectType.WeaponType weaponType;
    [Header("%비율")]
    public float Damage;
    [Header("근거리 = 0, 원거리 = 1")]
    [Range(0, 1)] public int WeaponType;
    [Header("원거리일 경우")]
    public float Range;
    public float bulletLifeTime;
    public float bulletSpeed;
}
