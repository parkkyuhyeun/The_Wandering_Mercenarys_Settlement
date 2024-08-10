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

    private Rigidbody2D rigid;
    private Collider2D playerCollider;
    private Collider2D weaponCollider;
    private bool isAttack = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        weaponCollider = weapon.GetComponent<Collider2D>();
    }

    private void Update()
    {
        //if (GameScenes.globalGameManager.isOpenSetting || GameScenes.globalGameManager.isGameOver || GameScenes.globalGameManager.isOpenShop || !GameScenes.globalGameManager.isGameStart)
        //{
        //    return;
        //}

        if(Input.GetMouseButton(0))
        {
            AttackStart();
        }
    }

    private void FixedUpdate()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");
        rigid.MovePosition(rigid.position + (new Vector2(xInput, yInput) * (moveSpeed * speedMultiplier)) * Time.deltaTime);
        var playerScale = rigid.gameObject.transform.localScale;
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

        }
        
    }
}
