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


    private void Awake()
    {
        GameScenes.globalEnemyController = this;  
        enemyCollider = GetComponent<Collider2D>();
        hand = GetComponentInChildren<GameObject>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        curHP = enemySO.MaxHP;
    }

    void Update()
    {
        
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
        }
    }

    public IEnumerator WaitASecond()
    {
        //몇초 기다렸다 달려가기
        yield return new WaitForSeconds(waitTime);
        letsGo = true;
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
        float timer = 0;
        while (true)
        {
            var bullet = GameScenes.globalPoolManager.SpawnWeapon(ObjectType.WeaponType.enemyBullet);
            bullet.transform.position = hand.transform.position;
            // 플레이어와 적 사이의 방향을 계산
            Vector2 direction = (player.transform.position - bullet.transform.position).normalized;

            // 적의 회전을 플레이어를 향하도록 설정
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

            // 플레이어 방향으로 이동
            bullet.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }
}
