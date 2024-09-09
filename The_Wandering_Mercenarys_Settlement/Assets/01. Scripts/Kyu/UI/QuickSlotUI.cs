using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotUI : MonoBehaviour
{
    [System.Serializable]
    public class ShowItem
    {
        public ObjectType.WeaponType weaponType;
        public ObjectType.PotionType potionType;
        public GameObject prefab;
    }

    [SerializeField] List<ShowItem> showItemList = new List<ShowItem>();
    [SerializeField] GameObject inventory;
    public int SlotId;

    public ObjectType.PotionType curPotion = ObjectType.PotionType.none;
    public ObjectType.WeaponType curWeapon = ObjectType.WeaponType.none;

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
            if(showItem.weaponType == type)
            {
                showItem.prefab.SetActive(true);
                curWeapon = showItem.weaponType;
                break;
            }
        }
        GameScenes.globalPlayerController.ShowWeapon(type);
    }

    public void ItemOnQuickslot(ObjectType.PotionType type)
    {
        foreach(var showItem in showItemList)
        {
            if(showItem.potionType == type)
            {
                showItem.prefab.SetActive(true);
                break;
            }
        }
        curPotion = type;
    }
}
