using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="StatsSO", menuName ="PlayerStats")]
public class PlayerSO : ScriptableObject
{
    public float MaxHP;
    public float CurHP;
    public float Damage;
    public int Level;
}
