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
    [Header("UI")]
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject clickUI;

    [Header("������Ʈ")]
    [SerializeField] GameObject merchant;

    [Header("���� �� ����")]
    [SerializeField] List<MonsterEntry> monsters;

    [Header("���� �ð� ����(�ʴ����� ���)")]
    public float dayLength; // ���� ���� �ʴ����� ���
    public float nightLength; // ���� ���� �ʴ����� ���

    public bool isNight;

    private float timer;
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
        if (!isNight && playerController.isAroundMerchant())
        {
            clickUI.SetActive(true);
            Debug.Log("���ΰ� ��������");
            if (Input.GetKeyDown(KeyCode.F))
            {
                print("F");
                shopUI.SetActive(true);
                clickUI.SetActive(false);
            }
        }
        if (!playerController.isAroundMerchant())
        {
            clickUI.SetActive(false);
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
        shopUI.SetActive(false);
        clickUI.SetActive(false);


        //StartMonsterWave();
    }

    public void StartMonsterWave(ObjectType.Monster[] monsterTypes)
    {
        for (int i = 0; i < monsterTypes.Length; i++)
            SetMonsterValues(ref monsterTypes[i]);

        for (int i = 0; i < monsterTypes.Length; i++)
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
        GameScenes.globalPoolManager.SpawnMonster(monster.name, monster.spawnDistance, player.transform.position, monster.spawnCount, monster.spawnTime);
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
