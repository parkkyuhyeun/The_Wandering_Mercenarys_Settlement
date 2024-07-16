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

    // 생성자는 필요에 따라 추가할 수 있습니다.
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
    [Header("오브젝트")]
    [SerializeField] GameObject merchant;
    [SerializeField] GameObject monsterParent;

    [Header("몬스터 값 설정")]
    [SerializeField] List<MonsterEntry> monsters;

    [Header("낮밤 시간 길이(초단위로 계산)")]
     public float dayLength; // 낮의 길이 초단위로 계산
     public float nightLength; // 밤의 길이 초단위로 계산
    
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
         SpawnMerchant(); // 낮에 상인 소환
     }

     void StartNight()
     {
         isNight = true;
         timer = 0;
         RemoveMerchant(); // 상인 제거
     }

    public IEnumerator StartMonsterWave(int monsterCnt, ObjectType.Monster monster1, ObjectType.Monster monster2, ObjectType.Monster monster3)
    {
        for (int i = 0; i < monsterCnt; i++)
        {
            SetMonsterValues(ref monster1);
            SetMonsterValues(ref monster2);
            SetMonsterValues(ref monster3);
            //몬스터 생성 로직
            StartCoroutine(SpawnMonster(monster1));
            StartCoroutine(SpawnMonster(monster2));
            StartCoroutine(SpawnMonster(monster3));
        }
        yield return null;
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
