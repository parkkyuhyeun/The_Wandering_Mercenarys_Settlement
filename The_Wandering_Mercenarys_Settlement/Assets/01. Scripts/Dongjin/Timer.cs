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
    [SerializeField] public List<MonsterEntry> monsters;

    [Header("���� �ð� ����(�ʴ����� ���)")]
     public float dayLength; // ���� ���� �ʴ����� ���
     public float nightLength; // ���� ���� �ʴ����� ���

    private int waveCount = 0;
     private float timer;
     private bool isNight;
    private GameObject player;
    private PlayerController playerController;

    private void Awake()
    {
        GameScenes.globalTimer = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        waveCount = 0;
    }

    void Start()
     {
         StartDay();
     }

     void Update()
     {
        //if (!GameScenes.globalGameManager.isGameStart)
        //{
        //    return;
        //}
         timer += Time.deltaTime;
         if (isNight && timer > nightLength)
         {
             StartDay();
         }
         else if (!isNight && timer > dayLength)
         {
             StartNight();
         }
         if(!isNight && playerController.isAroundMerchant())
         {

            clickUI.SetActive(true);
            Debug.Log("���ΰ� ��������");
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                shopUI.SetActive(true);
                Debug.Log("���� ����");
            }
         }
        if (!playerController.isAroundMerchant())
        {
            
            clickUI.SetActive(false);
            Debug.Log("���ΰ� �����");
            if (shopUI.activeSelf)
            {
                shopUI.SetActive(false);
                Debug.Log("���� ����");
            }
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

        StartMonsterWave(GameScenes.globalWaveManager.MonstersInWaves(waveCount));
        waveCount++;
    }

    public void StartMonsterWave(ObjectType.Monster[] monsterTypes)
    {
       for(int i = 0; i < monsterTypes.Length; ++i)
            StartCoroutine(SpawnMonster(monsterTypes[i]));
    }

    IEnumerator SpawnMonster(ObjectType.Monster monster)
    {
        //Ǯ�Ŵ��� ȣ��
        GameScenes.globalPoolManager.SpawnMonster(monster.name, monster.spawnDistance, player.transform.position, monster.spawnCount);
        yield return new WaitForSeconds(monster.spawnTime);
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
