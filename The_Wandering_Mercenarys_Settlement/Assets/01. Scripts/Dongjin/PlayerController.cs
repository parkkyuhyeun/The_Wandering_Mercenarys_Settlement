using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class PlayerWeapon
    {
        public ObjectType.WeaponType weaponType;
        public GameObject weaponPrefab;

        public PlayerWeapon(ObjectType.WeaponType weaponType, GameObject weaponPrefab)
        {
            this.weaponType = weaponType;
            this.weaponPrefab = weaponPrefab;
        }
    }


    [Header("속도값 조절")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedMultiplier;

    [Header("상인 관련")]
    [SerializeField] private Collider2D merchantRange;
    [SerializeField] private float merchantAndPlayerDistance;

    [Header("무기")]
    [SerializeField] public List<PlayerWeapon> weapons;

    [Header("스탯")]
    [SerializeField] public PlayerSO playerSO;

    private Rigidbody2D rigid;
    public Collider2D playerCollider;
    private bool isFinishCoolDown = true;
    private Animator anim;
    public float curHP;
    private QuickSlotUI quickSlotUI;
    private List<QuickSlotUI> quickSlotUIs = new List<QuickSlotUI>();

    private void Awake()
    {
        GameScenes.globalPlayerController = this;
        rigid = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        curHP = playerSO.MaxHP;
        GameScenes.globalLevelManager.LevelUp(ref playerSO);
        QuickSlotUI[] quicks = GameObject.FindObjectsOfType<QuickSlotUI>();
        foreach(var q in quicks)
        {
            quickSlotUIs.Add(q);
        }
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

        LevelUpManage();
    }

    public void LevelUpManage()
    {
        if (playerSO.curEXP >= playerSO.nextEXP)
        {
            //exp 초기화 작업
            playerSO.curEXP = playerSO.curEXP - playerSO.nextEXP;
            GameScenes.globalLevelManager.LevelUp(ref playerSO);
            PlayerStatSetting();
        }
    }

    public void PlayerStatSetting()
    {
        // 스탯을 레벨에 맞게 조정 (퍼센트 형식의 증가율 사용)
        playerSO.MaxHP = (int)(playerSO.MaxHP * (1 + (playerSO.hpIncreaseRate / 100f) * playerSO.Level));
        playerSO.Damage = (int)(playerSO.Damage * (1 + (playerSO.damageIncreaseRate / 100f) * playerSO.Level));
        playerSO.AttackCooldown *= 1 - (playerSO.cooldownDecreaseRate / 100f) * playerSO.Level;

        // 현재 체력을 최대 체력으로 갱신
        curHP = (int)playerSO.MaxHP;
    }

    public void PlayerStatSetting(float originalDamage)
    {
        // 스탯을 레벨에 맞게 조정 (퍼센트 형식의 증가율 사용)
        playerSO.MaxHP = (int)(playerSO.MaxHP * (1 + (playerSO.hpIncreaseRate / 100f) * playerSO.Level));
        playerSO.Damage = (int)(originalDamage * (1 + (playerSO.damageIncreaseRate / 100f) * playerSO.Level));
        playerSO.AttackCooldown *= 1 - (playerSO.cooldownDecreaseRate / 100f) * playerSO.Level;

        // 현재 체력을 최대 체력으로 갱신
        curHP = (int)playerSO.MaxHP;
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
        if (GameScenes.globalWeapon != null && GameScenes.globalWeapon.isContact)
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

    public void SpawnWeapon(ObjectType.WeaponType type)
    {
        GameObject weaponObj = GameScenes.globalPoolManager.SpawnWeapon(type, gameObject);
        weapons.Add(new PlayerWeapon(type, weaponObj));
    }

    public void ShowWeapon(ObjectType.WeaponType type)
    {
        foreach(var weapon in weapons)
        {
            if(weapon.weaponType == type) weapon.weaponPrefab.SetActive(true);
        }
    }

    public void DisableWeapon(ObjectType.WeaponType type)
    {
        foreach(var weapon in weapons)
        {
            if (weapon.weaponType == type) weapon.weaponPrefab.SetActive(false);
        }
    }

    internal void SwitchWeapon(int slotId)
    {
        foreach(var q in quickSlotUIs)
        {
            if(q.SlotId == slotId)
            {
                quickSlotUI = q;
                break;
            }
        }

        foreach(var weapon in weapons)
        {
            if(weapon.weaponType == quickSlotUI.curWeapon)
            {
                weapon.weaponPrefab.SetActive(true);
            }
            else
            {
                weapon.weaponPrefab.SetActive(false);
            }
        }
    }
}
