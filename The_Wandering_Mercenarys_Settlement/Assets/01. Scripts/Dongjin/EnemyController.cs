using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

        // 적의 회전을 플레이어를 향하도록 설정
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 플레이어 방향으로 이동
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
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
    {
        // 스탯을 레벨에 맞게 조정
        enemySO.MaxHP *= Mathf.Pow(enemySO.hpIncreaseRate, curLevel - 1);
        enemySO.Damage *= Mathf.Pow(enemySO.damageIncreaseRate, curLevel - 1);
        enemySO.AttackCooldown *= Mathf.Pow(enemySO.cooldownDecreaseRate, curLevel - 1);

        // 원거리 적일 경우 추가 스탯 조정
        if (enemySO.Series == 1)
        {
            enemySO.AttackDistance *= Mathf.Pow(enemySO.rangeIncreaseRate, curLevel - 1);
            enemySO.bulletLifeTime *= Mathf.Pow(enemySO.bulletLifeIncreaseRate, curLevel - 1);
            enemySO.bulletSpeed *= Mathf.Pow(enemySO.bulletSpeedIncreaseRate, curLevel - 1);
        }

        // 현재 체력을 최대 체력으로 갱신
        curHP = enemySO.MaxHP;
    }

    private void OnEnable()
    {
        StartCoroutine(WaitASecond());
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
                
                StartCoroutine(ShootAttack());
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

    private IEnumerator ShootAttack()
    {
        var bullet = GameScenes.globalPoolManager.SpawnWeapon(enemySO.weaponType);
        bullet.transform.position = hand.transform.position;
        var bulletCol = bullet.GetComponent<Collider2D>();
        float timer = 0;
        while (!(timer >= enemySO.bulletLifeTime))
        {
            if(bulletCol.Distance(GameScenes.globalPlayerController.playerCollider).distance < 0.1f)
            {
                Debug.Log("크킄 총알에 맞았구나");
                //총알 피격 이펙트

                GameScenes.globalPlayerController.TakeDamage(enemySO.Damage);
                break;
            }
            // 플레이어와 적 사이의 방향을 계산
            Vector2 direction = (player.transform.position - bullet.transform.position).normalized;

            // 적의 회전을 플레이어를 향하도록 설정
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

            // 플레이어 방향으로 이동
            bullet.transform.Translate(Vector2.right * enemySO.bulletSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        Debug.Log("총알 맞추기 실패");
        GameScenes.globalPoolManager.DespawnWeapon(bullet, enemySO.weaponType);
        yield return null;
    }
}
