using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] GameObject inventory;
    InventoryUI _inUi;
    UIManager _ui;
    List<QuickSlotUI> _quickSlot;

    public static Vector2 DefaultPos;
    public static Vector2 setPos;

    private InventoryUI.Item myItem;

    private void Awake()
    {
        _inUi = inventory.GetComponent<InventoryUI>();
        _ui = GameScenes.globalUIManager.GetComponent<UIManager>();
        _quickSlot = new List<QuickSlotUI>();
    }

    private void Start()
    {
        var objs = GameObject.FindGameObjectsWithTag("QuickSlot");
        foreach (var obj in objs)
        {
            _quickSlot.Add(obj.GetComponent<QuickSlotUI>());
        }
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
        foreach(var res in results)
        {
            Debug.Log($"Å¸°Ù: {res.gameObject.name}\n");
        }
        if (gameObject.CompareTag("Item"))
        {
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("ItemEquip") || result.gameObject.CompareTag("AnotherItem"))
                {
                    setPos = result.gameObject.transform.position;
                    foreach (var item in _inUi.currentItem)
                    {
                        if (gameObject.GetComponent<SpriteRenderer>().sprite.name == item.weaponPrefab.GetComponent<SpriteRenderer>().sprite.name)
                        {

                            if (result.gameObject.name[5] == '7')
                            {
                                _quickSlot[2].ItemOnQuickslot(item.weaponType);
                                Debug.Log($"{_quickSlot[2].name}¿¡ {item.weaponType.ToString()} ÀåÂø");
                            }
                            else if(result.gameObject.name[5] == '8')
                            {
                                _quickSlot[1].ItemOnQuickslot(item.weaponType);
                                Debug.Log($"{_quickSlot[1].name}¿¡ {item.weaponType.ToString()} ÀåÂø");
                            }
                            else if (result.gameObject.name[5] == '9')
                            {
                                _quickSlot[0].ItemOnQuickslot(item.weaponType);
                                Debug.Log($"{_quickSlot[0].name}¿¡ {item.weaponType.ToString()} ÀåÂø");
                            }


                            break;
                        }
                    }
                    return true;
                }
            }
        }
        else if (gameObject.CompareTag("Potion"))
        {
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("PotionEquip") || result.gameObject.CompareTag("AnotherPotion"))
                {
                    setPos = result.gameObject.transform.position;
                    ObjectType.PotionType curType = gameObject.name[5] == '6' ? ObjectType.PotionType.heal : ObjectType.PotionType.damageBoost;
                    _quickSlot[3].ItemOnQuickslot(curType);
                    return true;
                }
            }
        }
        return false;
    }

}
