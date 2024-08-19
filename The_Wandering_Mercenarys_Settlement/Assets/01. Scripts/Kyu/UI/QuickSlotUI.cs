using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotUI : MonoBehaviour
{
    [SerializeField] List<GameObject> showItemList = new List<GameObject>();
    [SerializeField] GameObject inventory;

    InventoryUI inven;

    private void Awake()
    {
        inven = inventory.GetComponent<InventoryUI>();
    }

    private void Update()
    {

    }

    public void Change(int cc, int dc)
    {
        StartCoroutine(ChangeItem(cc, dc));
    }

    public IEnumerator ChangeItem(int cc, int dc)
    {
        if (gameObject.name[9] - '0' == 1)
        {
            showItemList[cc - 1].SetActive(true);
            showItemList[dc - 1].SetActive(false);
        }
        if (gameObject.name[9] - '0' == 2)
        {
            showItemList[cc - 1].SetActive(true);
            showItemList[dc - 1].SetActive(false);
        }
        if (gameObject.name[9] - '0' == 3)
        {
            showItemList[cc - 1].SetActive(true);
            showItemList[dc - 1].SetActive(false);
        }
        if (gameObject.name[9] - '0' == 4)
        {
            showItemList[cc - 1].SetActive(true);
            showItemList[dc - 1].SetActive(false);
        }
        yield return null;
    }
}
