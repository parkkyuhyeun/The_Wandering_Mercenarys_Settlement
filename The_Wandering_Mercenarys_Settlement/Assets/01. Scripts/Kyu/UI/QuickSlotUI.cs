using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotUI : MonoBehaviour
{
    [SerializeField] List<GameObject> showItemList = new List<GameObject>();
    [SerializeField] GameObject inventory;

    InventoryUI _inven;

    private void Awake()
    {
        _inven = inventory.GetComponent<InventoryUI>();
    }

    private void Update()
    {
        if (gameObject.name[9] == '1')
        {
            if (_inven.currentItem[7] != 0) showItemList[_inven.currentItem[7] - 1].SetActive(true);
            else for (int i = 0; i < showItemList.Count; i++) showItemList[i].SetActive(false);
        }
        if (gameObject.name[9] == '2')
        {
            if (_inven.currentItem[8] != 0) showItemList[_inven.currentItem[8] - 1].SetActive(true);
            else for (int i = 0; i < showItemList.Count; i++) showItemList[i].SetActive(false);
        }
        if (gameObject.name[9] == '3')
        {
            if (_inven.currentItem[9] != 0) showItemList[_inven.currentItem[9] - 1].SetActive(true);
            else for (int i = 0; i < showItemList.Count; i++) showItemList[i].SetActive(false);
        }
        if (gameObject.name[9] == '4')
        {
            if (_inven.currentPotion[9] != 0) showItemList[_inven.currentPotion[9] - 1].SetActive(true);
            else for (int i = 0; i < showItemList.Count; i++) showItemList[i].SetActive(false);
        }
    }
}
