using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [System.Serializable]
    public class Item : PlayerController.PlayerWeapon
    {
        public int id;

        public Item(int id, ObjectType.WeaponType weaponType, GameObject weaponPrefab) : base(weaponType, weaponPrefab)
        {
            this.id = id;
            this.weaponType = weaponType;
            this.weaponPrefab = weaponPrefab;
        }
    }

    [System.Serializable]
    public class Potion
    {
        public int id;
        public ObjectType.PotionType potionType;

        public Potion(int id, ObjectType.PotionType potionType)
        {
            this.id = id;
            this.potionType = potionType;
        }
    }

    [SerializeField] GameObject inventoryUI;

    public List<GameObject> itemPrefabs = new List<GameObject>();
    public List<GameObject> potionPrefabs = new List<GameObject>();
    public List<Item> currentItem = new List<Item>();
    public List<Potion> currentPotion = new List<Potion>();

    private void Awake()
    {

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
