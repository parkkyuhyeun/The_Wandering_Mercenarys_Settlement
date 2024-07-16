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
    public int spawnCount; //���� ��

    // �����ڴ� �ʿ信 ���� �߰��� �� �ֽ��ϴ�.
    public MonsterEntry(ObjectType.MonsterType type, GameObject prefab, int spawnTime, int spawnDistance, int spawnCount)
    {
        this.type = type;
        this.prefab = prefab;
        this.spawnTime = spawnTime;
        this.spawnDistance = spawnDistance;
        this.spawnCount = spawnCount;
    }
}

public class Timer : MonoBehaviour
{
    [Header("������Ʈ")]
    [SerializeField] GameObject merchant;

    [Header("���� �� ����")]
    [SerializeField] List<MonsterEntry> monsters;

    [Header("���� �ð� ����(�ʴ����� ���)")]
     public float dayLength; // ���� ���� �ʴ����� ���
     public float nightLength; // ���� ���� �ʴ����� ���
    
     private float timer;
     private bool isNight;
    private bool isMerchantAround = false;
    private GameObject player;
    private PlayerController playerController;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void Start()
     {
         StartDay();
     }

     void Update()
     {
         timer += Time.deltaTime;
         if (isNight && timer > nightLength)
         {
             StartDay();
         }
         else if (!isNight && timer > dayLength)
         {
             StartNight();
         }
         if(!isNight && playerController.isAroundMerchant() && !isMerchantAround)
         {
            //���⿡ ���� ���� �ڵ� �߰�
            Debug.Log("���ΰ� ��������");
            isMerchantAround = true;
         }
        if (!playerController.isAroundMerchant() && isMerchantAround)
            isMerchantAround = false;
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
        ObjectType.Monster goblin = new ObjectType.Monster
        {
            name = ObjectType.MonsterType.Goblin,
            spawnTime = 1,
            spawnDistance = 5,
            spawnCount = 3
        };

        StartMonsterWave(new ObjectType.Monster[] { goblin });
    }

    public void StartMonsterWave(ObjectType.Monster[] monsterTypes)
    {
       for(int i = 0; i < monsterTypes.Length; i++)
            SetMonsterValues(ref monsterTypes[i]);
        
       for(int i = 0; i < monsterTypes.Length; i++)
            StartCoroutine(SpawnMonster(monsterTypes[i]));
        
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

        foreach (var item in monsters)
        {
            if (item.type == monster.name)
            {
                monster.spawnCount = item.spawnCount;
                break;
            }
        }
    }

    IEnumerator SpawnMonster(ObjectType.Monster monster)
    {
        //Ǯ�Ŵ��� ȣ��
        GameScenes.poolManager.SpawnMonster(monster.name, monster.spawnDistance, player.transform.position, monster.spawnCount, monster.spawnTime);
        yield return null;
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
