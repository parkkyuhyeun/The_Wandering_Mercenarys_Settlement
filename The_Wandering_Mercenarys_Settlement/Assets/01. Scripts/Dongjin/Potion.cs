using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private float healValue;
    [SerializeField] private float strengthValue;

    [SerializeField] public float healTimer;
    [SerializeField] public float damageTimer;

    [SerializeField] public QuickSlotUI quickSlot;

    public bool isActiveHeal = false;
    public bool isActiveDamage = false;
    private float originalDamage;

    private void Awake()
    {
        GameScenes.globalPotion = this;
    }

    private void Update()
    {
        
    }

    public void Healing()
    {
        GameScenes.globalPlayerController.curHP += GameScenes.globalPlayerController.playerSO.MaxHP * 0.01f * healValue;

    }

    public void DamageBoosting()
    {
        originalDamage = GameScenes.globalPlayerController.playerSO.Damage;
        GameScenes.globalPlayerController.playerSO.Damage += 0.01f * strengthValue;
    }

    public void EndDamageBoosting()
    {
        GameScenes.globalPlayerController.PlayerStatSetting(originalDamage);
    }

    public void ActiveHeal()
    {
        isActiveHeal = true;
    }

    public void ActiveDamage()
    {
        isActiveDamage = true;
    }


}
