using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public WeaponSO weaponSO;
    public ObjectType.WeaponType Type;
    public bool isContact = false;
    public GameObject EnemyObj;

    public Weapon(ObjectType.WeaponType type)
    {
        this.Type = type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isContact = true;
            EnemyObj = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isContact = false;
        }
    }

    public void SetType(ObjectType.WeaponType type)
    {
        this.Type = type;
    }
}
