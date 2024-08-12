using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] GameObject inventory;
    InventoryUI inUi;

    public static Vector2 DefaultPos;
    public static Vector2 setPos;

    public int DefaultValue = 0;
    public int CurrentValue;

    public int itemCode;

    private void Awake()
    {
        inUi = inventory.GetComponent<InventoryUI>();
        itemCode = gameObject.name[5] - '0';
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        DefaultPos = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = eventData.position;
        this.transform.position = currentPos;

    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (!IsPointerOverUI())
        {
            this.transform.position = DefaultPos;
        }
        else
        {
            this.transform.position = setPos;
            DefaultValue = CurrentValue;
        }
    }

    private bool IsPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (gameObject.CompareTag("Item"))
            {
                if (results[i].gameObject.CompareTag("ItemEquip") || results[i].gameObject.CompareTag("AnotherItem"))
                {
                    CurrentValue = results[i].gameObject.name[5] - '0';
                    if (inUi.currentItem[CurrentValue] == 0)
                    {
                        setPos = results[i].gameObject.transform.position;
                        inUi.currentItem[CurrentValue] = itemCode;
                        inUi.currentItem[DefaultValue] = 0;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (gameObject.CompareTag("Potion"))
            {
                if (results[i].gameObject.CompareTag("PotionEquip") || results[i].gameObject.CompareTag("AnotherPotion"))
                {
                    CurrentValue = results[i].gameObject.name[5] - '0';
                    if (inUi.currentPotion[CurrentValue] == 0)
                    {
                        setPos = results[i].gameObject.transform.position;
                        inUi.currentPotion[CurrentValue] = itemCode;
                        inUi.currentPotion[DefaultValue] = 0;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        return false;
    }
}
