using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable] // �ν����Ϳ��� ���̰� �ϱ� ���� �ʿ�
public class MonsterEntry
{
    public ObjectType.MonsterType type; // ���� ����
    public GameObject prefab; // ���� ������
    public int spawnTime; // ���� ���� �ð�
    public int spawnDistance; // ���� ���� �Ÿ�

    // �����ڴ� �ʿ信 ���� �߰��� �� �ֽ��ϴ�.
    public MonsterEntry(ObjectType.MonsterType type, GameObject prefab, int spawnTime, int spawnDistance)
    {
        this.type = type;
        this.prefab = prefab;
        this.spawnTime = spawnTime;
        this.spawnDistance = spawnDistance;
    }
}

public class Timer : MonoBehaviour
{
    [Header("������Ʈ")]
    [SerializeField] GameObject merchant;
    [SerializeField] GameObject monsterParent;

    [Header("���� �� ����")]
    [SerializeField] List<MonsterEntry> monsters;

    [Header("���� �ð� ����(�ʴ����� ���)")]
     public float dayLength; // ���� ���� �ʴ����� ���
     public float nightLength; // ���� ���� �ʴ����� ���
    
     private float timer;
     private bool isNight;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
     {
         StartDay();
     }

     void Update()
     {
         timer += Time.deltaTime * 60f;
         if (isNight && timer > nightLength)
         {
             StartDay();
         }
         else if (!isNight && timer > dayLength)
         {
             StartNight();
         }
     }

     void StartDay()
     {
         isNight = false;
         timer = 0;
         SpawnMerchant(); // ���� ���� ��ȯ
     }

     void StartNight()
     {
         isNight = true;
         timer = 0;
         RemoveMerchant(); // ���� ����
     }

    public IEnumerator StartMonsterWave(int monsterCnt, ObjectType.Monster monster1, ObjectType.Monster monster2, ObjectType.Monster monster3)
    {
        for (int i = 0; i < monsterCnt; i++)
        {
            SetMonsterValues(ref monster1);
            SetMonsterValues(ref monster2);
            SetMonsterValues(ref monster3);
            //���� ���� ����
            StartCoroutine(SpawnMonster(monster1));
            StartCoroutine(SpawnMonster(monster2));
            StartCoroutine(SpawnMonster(monster3));
        }
        yield return null;
    }

    // ������ �� ����
    private void SetMonsterValues(ref ObjectType.Monster monster)
    {
        foreach (var item in monsters)
        {
            if (item.type == monster.name)
            {
                monster.spawnTime = item.spawnTime;
                break;
            }
        }

        foreach (var item in monsters)
        {
            if (item.type == monster.name)
            {
                monster.spawnDistance = item.spawnDistance;
                break;
            }
        }
    }

    IEnumerator SpawnMonster(ObjectType.Monster monster)
    {
        GameObject monsterObj = null;
        Transform monsterTrm = null;
        FindMonsterObj(monster.name, ref monsterObj);
        Instantiate(monsterObj, monsterTrm.position, Quaternion.identity, monsterParent.transform);
        yield return null;
    }

    private void FindMonsterObj(ObjectType.MonsterType curMonsterType, ref GameObject monsterObj)
    {
        foreach(var item in monsters)
        {
            if(item.type == curMonsterType)
            {
                monsterObj = item.prefab;
            }
        }
    }

    void SpawnMerchant()
     {
         merchant.SetActive(true);
     }

     void RemoveMerchant()
     {
        merchant.SetActive(false);
     }



}
