using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    bool isActiveMenu = false;


    private void Awake()
    {
        GameScenes.globalKeyManager = this;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if(isActiveMenu)
            {
                isActiveMenu = false;
                //GameScenes.globalUIManager.
            }
            else
            {
                isActiveMenu = true;
            }
        }

        if(Input.GetKey(KeyCode.Alpha1))
        {
            GameScenes.globalPlayerController.SwitchWeapon(1);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            GameScenes.globalPlayerController.SwitchWeapon(2);
        }

        if(Input.GetKeyUp(KeyCode.Alpha3))
        {
            GameScenes.globalPlayerController.SwitchWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GameScenes.globalPotion.quickSlot.curPotion == ObjectType.PotionType.heal && !GameScenes.globalPotion.isActiveHeal)
            {
                GameScenes.globalPotion.Healing();
                GameScenes.globalPotion.ActiveHeal();
                StartCoroutine(healCoolDown());
            }
            else if (GameScenes.globalPotion.quickSlot.curPotion == ObjectType.PotionType.damageBoost && !GameScenes.globalPotion.isActiveDamage)
            {
                GameScenes.globalPotion.DamageBoosting();
                GameScenes.globalPotion.ActiveDamage();
                StartCoroutine(damageCoolDown());
            }
        }
    }

    private IEnumerator healCoolDown()
    {
        yield return new WaitForSeconds(GameScenes.globalPotion.healTimer);
        GameScenes.globalPotion.isActiveHeal = false;
    }

    private IEnumerator damageCoolDown()
    {
        yield return new WaitForSeconds(GameScenes.globalPotion.damageTimer);
        GameScenes.globalPotion.isActiveDamage = false;
    }
}
