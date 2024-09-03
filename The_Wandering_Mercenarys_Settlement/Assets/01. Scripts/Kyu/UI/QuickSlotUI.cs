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
        if (gameObject.name[9] == '1')
        {
            if (_inven.currentItem[7].id != 0) showItemList[_inven.currentItem[7].id - 1].SetActive(true);
            else for (int i = 0; i < showItemList.Count; i++) showItemList[i].SetActive(false);
        }
        if (gameObject.name[9] == '2')
        {
            if (_inven.currentItem[8].id != 0) showItemList[_inven.currentItem[8].id - 1].SetActive(true);
            else for (int i = 0; i < showItemList.Count; i++) showItemList[i].SetActive(false);
        }
        if (gameObject.name[9] == '3')
        {
            if (_inven.currentItem[9].id != 0) showItemList[_inven.currentItem[9].id - 1].SetActive(true);
            else for (int i = 0; i < showItemList.Count; i++) showItemList[i].SetActive(false);
        }
        if (gameObject.name[9] == '4')
        {
            if (_inven.currentPotion[9].id != 0) showItemList[_inven.currentPotion[9].id - 1].SetActive(true);
            else for (int i = 0; i < showItemList.Count; i++) showItemList[i].SetActive(false);
        }
    }

    public void ItemOnQuickslot(ObjectType.WeaponType type)
    {
        foreach(var showItem in showItemList)
        {
            showItem.SetActive(false);
        }
        GameScenes.globalPlayerController.ShowWeapon(type);
    }

    public void ItemOnQuickslot(ObjectType.PotionType type)
    {
        foreach (var showItem in showItemList)
        {
            showItem.SetActive(false);
        }
        curPotion = type;
    }
}
