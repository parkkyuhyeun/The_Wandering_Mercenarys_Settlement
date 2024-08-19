using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    [Header("UI")]
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject clickUI;

    [Header("오브젝트")]
    [SerializeField] GameObject merchant;

    [Header("몬스터 값 설정")]
    [SerializeField] public List<MonsterEntry> monsters;

    [Header("낮밤 시간 길이(초단위로 계산)")]
     public float dayLength; // 낮의 길이 초단위로 계산
     public float nightLength; // 밤의 길이 초단위로 계산

    [Header("스폰 포인트")]
    [SerializeField] private GameObject merchantSpawnPoint;
    [SerializeField] private GameObject merchantMovePoint;
    [SerializeField] private GameObject merchantEndPoint;

    private int waveCount = 0;
     private float timer;
     public bool isNight;
    private GameObject player;
    private PlayerController playerController;
    private bool isFirstMove;
    private Animator anim;

    private void Awake()
    {
        GameScenes.globalTimer = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        waveCount = 0;
        anim = merchant.GetComponent<Animator>();
    }

    void Start()
     {
         StartDay();
        GoToSpawnPoint();
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
            Debug.Log("상인과 접촉했음");
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                shopUI.SetActive(true);
                Debug.Log("상점 오픈");
            }
         }
        if (!playerController.isAroundMerchant())
        {
            
            clickUI.SetActive(false);
            Debug.Log("상인과 헤어짐");
            if (shopUI.activeSelf)
            {
                shopUI.SetActive(false);
                Debug.Log("상점 닫음");
            }
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
        //풀매니저 호출
        GameScenes.globalPoolManager.SpawnMonster(monster.name, monster.spawnDistance, player.transform.position, monster.spawnCount);
        yield return new WaitForSeconds(monster.spawnTime);
    }

    void SpawnMerchant()
     {
        isFirstMove = false;
        merchant.SetActive(true);
        MoveRight(merchantMovePoint.transform.position);
     }

     void RemoveMerchant()
     {
        isFirstMove = true;
        MoveRight(merchantEndPoint.transform.position);
        merchant.SetActive(false);
    }

    private void MoveRight(Vector3 targetPosition)
    {

        //걷기 애니메이션 처리
        //anim.SetTrigger("walk");
        //merchantSpawnPoint에서 merchantMovePoint 까지 일정 시간동안 자연스럽게 이동하기
        StartCoroutine(MoveToPosition(targetPosition, 1.5f));
        //아이들 애니메이션 전환
        //anim.SetTrigger("idle");
        
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = merchant.transform.position;

        while (time < duration)
        {
            merchant.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        merchant.transform.position = targetPosition;
        if (isFirstMove) GoToSpawnPoint();
    }

    private void GoToSpawnPoint()
    {
        merchant.transform.position = merchantSpawnPoint.transform.position;
    }
}
