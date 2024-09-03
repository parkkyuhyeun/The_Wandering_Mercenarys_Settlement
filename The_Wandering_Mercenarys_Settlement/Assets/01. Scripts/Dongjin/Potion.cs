using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private float healValue;
    [SerializeField] private float strengthValue;

    [SerializeField] private QuickSlotUI quickSlot;

    private bool isActiveHeal = false;
    private bool isActiveDamage = false;

    private void Awake()
    {
        GameScenes.globalPotion = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(quickSlot.curPotion == ObjectType.PotionType.heal)
            {
                Healing();
            }
            else if(quickSlot.curPotion == ObjectType.PotionType.damageBoost)
            {
                DamageBoosting();
            }
        }
    }

    private void Healing()
    {
        
    }

    private void DamageBoosting()
    {
        
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
