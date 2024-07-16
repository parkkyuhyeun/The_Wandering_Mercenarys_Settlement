using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable] // 인스펙터에서 보이게 하기 위해 필요
public class MonsterEntry
{
    public ObjectType.MonsterType type; // 몬스터 유형
    public GameObject prefab; // 몬스터 프리팹
    public int spawnTime; // 몬스터 스폰 시간
    public int spawnDistance; // 몬스터 스폰 거리
    public int spawnCount; //몬스터 수

    // 생성자는 필요에 따라 추가할 수 있습니다.
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
    [Header("오브젝트")]
    [SerializeField] GameObject merchant;

    [Header("몬스터 값 설정")]
    [SerializeField] List<MonsterEntry> monsters;

    [Header("낮밤 시간 길이(초단위로 계산)")]
     public float dayLength; // 낮의 길이 초단위로 계산
     public float nightLength; // 밤의 길이 초단위로 계산
    
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
            //여기에 상점 관련 코드 추가
            Debug.Log("상인과 접촉했음");
            isMerchantAround = true;
         }
        if (!playerController.isAroundMerchant() && isMerchantAround)
            isMerchantAround = false;
     }

     void StartDay()
     {
         isNight = false;
         timer = 0;
         SpawnMerchant(); // 낮에 상인 소환
     }

     void StartNight()
     {
         isNight = true;
         timer = 0;
         RemoveMerchant(); // 상인 제거
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

    // 몬스터의 값 세팅
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
        //풀매니저 호출
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
