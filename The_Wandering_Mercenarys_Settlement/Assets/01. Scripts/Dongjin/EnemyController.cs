using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("스탯")]
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int waitTime;

    private float curHP;
    private GameObject player;
    private bool letsGo = false;
    private Collider2D enemyCollider;
    private bool isPossibleAttack = true;
    private GameObject hand;
    public int curLevel = 1;


    private void Awake()
    {
        GameScenes.globalEnemyController = this;  
        enemyCollider = GetComponent<Collider2D>();
        hand = GetComponentInChildren<Transform>().gameObject;

    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        curHP = enemySO.MaxHP;
    }

    void Update()
    {
        if (!letsGo) return;
        AttackPlayer();
    }

    private void FixedUpdate()
    {
        if (!letsGo) return;
        FindPlayerAndFollow();
    }

    private void FindPlayerAndFollow()
    {
        // 플레이어와 적 사이의 방향을 계산
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // 적이 플레이어의 위치에 따라 좌우로 뒤집히도록 설정
        if (direction.x > 0)
        {
            // 플레이어가 오른쪽에 있을 때
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // 플레이어가 왼쪽에 있을 때
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // 플레이어 방향으로 이동
        transform.Translate((direction * moveSpeed) * Time.deltaTime);
    }

    public void TakeDamage(float Damage)
    {
        curHP -= Damage;
        if(curHP < 0)
        {
            //풀에 다시 넣기
            GameScenes.globalPoolManager.DespawnMonster(gameObject, enemySO.monsterType);
            //플레이어 경험치 획득
            GameScenes.globalPlayerController.SetEXP(GameScenes.globalLevelManager.GetEXP(curLevel, enemySO.GiveExp));
            Debug.Log("몬스터: 으아앜");
            //플레이어 코인 획득

            GameScenes.globalCoinManager.coin.coinContain[enemySO.CoinLevel] += enemySO.GiveCoin * Mathf.Max(1, curLevel);
            GameScenes.globalCoinManager.UpdateCoin(enemySO.CoinLevel);
            Debug.Log($"코인을 {enemySO.GiveCoin * Mathf.Max(1, curLevel)} 만큼 획득했다 \n현재코인: {GameScenes.globalCoinManager.coin}");
        }
    }

    public IEnumerator WaitASecond()
    {
        //몇초 기다렸다 달려가기
        yield return new WaitForSeconds(waitTime);
        curLevel = GameScenes.globalWaveManager.GetCurWave();
        EnemyStatSetting();
        letsGo = true;
    }

    private void EnemyStatSetting()
    {// 스탯을 레벨에 맞게 조정 (퍼센트 형식의 증가율 사용)
        enemySO.MaxHP = (int)(enemySO.MaxHP * (1 + (enemySO.hpIncreaseRate / 100f) * curLevel));
        enemySO.Damage = (int)(enemySO.Damage * (1 + (enemySO.damageIncreaseRate / 100f) * curLevel));
        enemySO.AttackCooldown *= 1 - (enemySO.cooldownDecreaseRate / 100f) * curLevel;

        // 원거리 적일 경우 추가 스탯 조정
        if (enemySO.Series == 1)
        {
            enemySO.AttackDistance = (int)(enemySO.AttackDistance * (1 + (enemySO.rangeIncreaseRate / 100f) * curLevel));
            enemySO.bulletLifeTime = (int)(enemySO.bulletLifeTime * (1 + (enemySO.bulletLifeIncreaseRate / 100f) * curLevel));
            enemySO.bulletSpeed = (int)(enemySO.bulletSpeed * (1 + (enemySO.bulletSpeedIncreaseRate / 100f) * curLevel)); ;
        }

        // 현재 체력을 최대 체력으로 갱신
        curHP = enemySO.MaxHP;
    }

    private void OnEnable()
    {
        StartCoroutine(WaitASecond());
        Debug.Log($"적 활성화 후 기브 코인: {enemySO.GiveCoin * curLevel}");
    }

    public void AttackPlayer()
    {
        if (enemySO.Series == 0)
        {
            if (enemyCollider.Distance(GameScenes.globalPlayerController.playerCollider).distance < enemySO.AttackDistance && isPossibleAttack)
            {
                //공격 애니메이션
                GameScenes.globalPlayerController.TakeDamage(enemySO.Damage);
                isPossibleAttack = false;
                StartCoroutine(AttackReload());
            }
        }
        else if(enemySO.Series == 1)
        {
            if (enemyCollider.Distance(GameScenes.globalPlayerController.playerCollider).distance < enemySO.AttackDistance && isPossibleAttack)
            {
                //공격 애니메이션
                
                StartCoroutine(GameScenes.globalBullet.ShootAttack(enemySO, hand, player));
                isPossibleAttack = false;
                StartCoroutine(AttackReload());
            }
        }
        //보류
        //else if (enemySO.Series == 2)
        //{
        //    if (enemyCollider.Distance(GameScenes.globalPlayerController.playerCollider).distance < enemySO.AttackDistance && isPossibleAttack)
        //    {

        //    }
        //        isPossibleAttack = false;
        //    StartCoroutine(AttackReload());
        //}
    }

    private IEnumerator AttackReload()
    {
        yield return new WaitForSeconds(enemySO.AttackCooldown);
        isPossibleAttack = true;
    }
}
