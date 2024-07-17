using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�ӵ��� ����")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedMultiplier;

    [Header("���� ����")]
    [SerializeField] private Collider2D merchantRange;
    [SerializeField] private float merchantAndPlayerDistance;

    private Rigidbody2D rigid;
    private Collider2D playerCollider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        var xInput = Input.GetAxisRaw("Horizontal");
        var yInput = Input.GetAxisRaw("Vertical");
        rigid.MovePosition(rigid.position + (new Vector2(xInput, yInput) * (moveSpeed * speedMultiplier)) * Time.deltaTime);
    }

    public bool isAroundMerchant()
    {
        if (merchantRange.Distance(playerCollider).distance < merchantAndPlayerDistance) return true; 
        else return false;
    }
}
