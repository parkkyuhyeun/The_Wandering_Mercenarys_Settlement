using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] GameObject inventory;
    InventoryUI _inUi;
    UIManager _ui;

    public static Vector2 DefaultPos;
    public static Vector2 setPos;

    private InventoryUI.Item myItem;

    private void Awake()
    {
        _inUi = inventory.GetComponent<InventoryUI>();
        _ui = GameScenes.globalUIManager.GetComponent<UIManager>();
    }

    private void Update()
    {
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
        }
    }

    private bool IsPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

       foreach (RaycastResult result in results)
        {
            if (gameObject.CompareTag("Item"))
            {
                if (result.gameObject.CompareTag("ItemEquip") || result.gameObject.CompareTag("AnotherItem"))
                {
                    setPos = result.gameObject.transform.position;
                    foreach(var item in _inUi.currentItem)
                    {
                        if(item.weaponPrefab.GetComponent<SpriteRenderer>().GetInstanceID() == result.gameObject.GetComponent<SpriteRenderer>().GetInstanceID())
                        {
                            gameObject.GetComponentInParent<Transform>().GetComponent<QuickSlotUI>().ItemOnQuickslot(item.weaponType);
                            myItem = item;
                            break;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (gameObject.CompareTag("Potion"))
            {
                if (result.gameObject.CompareTag("PotionEquip") || result.gameObject.CompareTag("AnotherPotion"))
                {
                    setPos = result.gameObject.transform.position;
                    
                    //gameObject.GetComponentInParent<Transform>().GetComponent<QuickSlotUI>().ItemOnQuickslot();
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }

}
