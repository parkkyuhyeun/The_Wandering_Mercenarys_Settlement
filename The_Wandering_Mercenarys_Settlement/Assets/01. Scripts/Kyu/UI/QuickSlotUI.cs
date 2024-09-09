using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotUI : MonoBehaviour
{
    [SerializeField] List<GameObject> showItemList = new List<GameObject>();
    [SerializeField] GameObject inventory;

    public ObjectType.PotionType curPotion = ObjectType.PotionType.none;

    InventoryUI _inven;

    private void Awake()
    {
        _inven = inventory.GetComponent<InventoryUI>();
    }

    private void Update()
    {
        
    }

    public void ItemOnQuickslot(ObjectType.WeaponType type)
    {
        foreach (var showItem in showItemList)
        {
            showItem.SetActive(true);
            if (showItem.GetComponent<SpriteRenderer>().sprite.name == type.ToString())
            {
                break;
            }
            showItem.SetActive(false);
        }
        GameScenes.globalPlayerController.ShowWeapon(type);
    }

    public void ItemOnQuickslot(ObjectType.PotionType type)
    {
        if(type == ObjectType.PotionType.heal) showItemList[0].SetActive(true);
        else if(type == ObjectType.PotionType.damageBoost) showItemList[1].SetActive(true);
        curPotion = type;
    }
}
