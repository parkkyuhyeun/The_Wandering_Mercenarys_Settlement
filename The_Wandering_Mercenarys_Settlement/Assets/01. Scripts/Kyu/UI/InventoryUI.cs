using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;

    public List<int> currentItem = new List<int>();
    public List<int> currentPotion = new List<int>();

    private void Awake()
    {
        for(int i = 0; i < 10; i++)
        {
            currentItem.Add(0);
        }
        for(int i = 0; i < 10; i++)
        {
            currentPotion.Add(0);
        }
    }

    public void OpenUI()
    {
        inventoryUI.SetActive(true);
    }

    public void CloseUI()
    {
        inventoryUI.SetActive(false);
    }
}
