using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("속도값 조절")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedMultiplier;

    [Header("상인 관련")]
    [SerializeField] private Collider2D merchantRange;
    [SerializeField] private float merchantAndPlayerDistance;

    [Header("무기")]
    [SerializeField] private GameObject weapon;

    [Header("스탯")]
    [SerializeField] private PlayerSO playerSO;

    private Rigidbody2D rigid;
    public Collider2D playerCollider;
    private bool isFinishCoolDown = true;
    private Animator anim;
    public float curHP;

    private void Awake()
    {
        GameScenes.globalPlayerController = this;
        rigid = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        curHP = playerSO.MaxHP;
    }

    private void Update()
    {
        //if (GameScenes.globalGameManager.isOpenSetting || GameScenes.globalGameManager.isGameOver || GameScenes.globalGameManager.isOpenShop || !GameScenes.globalGameManager.isGameStart)
        //{
        //    return;
        //}
        if (isFinishCoolDown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AttackStart();
                isFinishCoolDown = false;
                StartCoroutine(FinishCoolDown());
            }
        }

        if(playerSO.curEXP > playerSO.nextEXP)
        {
            GameScenes.globalLevelManager.LevelUp(ref playerSO);

        }
    }

    private void PlayerStatSetting()
    {
        // 스탯을 레벨에 맞게 조정
        playerSO.MaxHP *= Mathf.Pow(playerSO.hpIncreaseRate, playerSO.Level);
        playerSO.Damage *= Mathf.Pow(playerSO.damageIncreaseRate, playerSO.Level);
        playerSO.AttackCooldown *= Mathf.Pow(playerSO.cooldownDecreaseRate, playerSO.Level);

        // 현재 체력을 최대 체력으로 갱신
        curHP = playerSO.MaxHP;
    }

    private IEnumerator FinishCoolDown()
    {
        yield return new WaitForSeconds(playerSO.AttackCooldown);
        isFinishCoolDown=true;
            
    }

    private void FixedUpdate()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");
        rigid.MovePosition(rigid.position + (new Vector2(xInput, yInput) * (moveSpeed * speedMultiplier)) * Time.deltaTime);
        var playerScale = rigid.gameObject.transform.localScale;
        if (xInput != 0 || yInput != 0)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
        if (xInput < 0)
        {
            playerScale = new Vector3(-1, playerScale.y, playerScale.z);
        }
        else if(xInput > 0)
        {
            playerScale = new Vector3(1, playerScale.y, playerScale.z);
        }

        rigid.gameObject.transform.localScale = playerScale; 
    }

    public bool isAroundMerchant()
    {
        if (merchantRange.Distance(playerCollider).distance < merchantAndPlayerDistance) return true; 
        else return false;
    }

    private void AttackStart()
    {
        //애니메이션
        
        //닿았으면 데미지 주기
        if (GameScenes.globalWeapon.isContact)
        {
            Debug.Log("데미지 빵야");
            GameScenes.globalWeapon.EnemyObj.GetComponent<EnemyController>().TakeDamage(playerSO.Damage);
        }
        
    }

    public void TakeDamage(float Damage)
    {
        curHP -= Damage;
        Debug.Log("플레이어: 아야");
        if(curHP < 0)
        {
            Debug.Log("플레이어 사망");
            //사망 처리
        }
    }

    public void SetEXP(float exp)
    {
        playerSO.curEXP += exp;
    }
}
