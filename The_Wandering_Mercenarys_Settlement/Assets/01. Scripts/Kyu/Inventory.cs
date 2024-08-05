using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;

    public void OpenUI()
    {
        inventoryUI.SetActive(true);
    }

    public void CloseUI()
    {
        inventoryUI.SetActive(false);
    }
}
