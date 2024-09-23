using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Bullet : MonoBehaviour
{
    public bool isContact;
    public GameObject EnemyObj;

    private void Awake()
    {
        isContact = false;
    }

    public IEnumerator ShootAttack(EnemySO enemySO, GameObject hand, GameObject player)
    {
        transform.position = hand.transform.position;
        var bulletCol = transform.GetComponent<Collider2D>();
        float timer = 0;

        // 플레이어와 적 사이의 방향을 계산
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // 적의 회전을 플레이어를 향하도록 설정
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        while (!(timer >= enemySO.bulletLifeTime))
        {
            if (bulletCol.Distance(GameScenes.globalPlayerController.playerCollider).distance < 0.1f)
            {
                Debug.Log("크킄 총알에 맞았구나");
                //총알 피격 이펙트

                GameScenes.globalPlayerController.TakeDamage(enemySO.Damage);
                break;
            }

            // 플레이어 방향으로 이동
            transform.Translate(Vector2.right * enemySO.bulletSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        Debug.Log("총알 사라짐");
        GameScenes.globalPoolManager.DespawnWeapon(gameObject, enemySO.weaponType);
        yield return null;
    }

    public IEnumerator ShootAttack(WeaponSO weaponSO, GameObject weaponPos, Vector3 mousePos)
    {
        transform.position = weaponPos.transform.position;
        var bulletCol = GetComponent<Collider2D>();
        float timer = 0;

        // 플레이어와 적 사이의 방향을 계산
        Vector2 direction = (mousePos - transform.position).normalized;

        // 적의 회전을 플레이어를 향하도록 설정
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        while (!(timer >= weaponSO.bulletLifeTime))
        {
            if (isContact)
            {
                Debug.Log("크킄 총알에 맞았구나");
                //총알 피격 이펙트

                EnemyObj.GetComponent<EnemyController>().TakeDamage(weaponSO.Damage);
                break;
            }

            // 플레이어 방향으로 이동
            transform.Translate(Vector2.right * weaponSO.bulletSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        Debug.Log("총알 사라짐");
        GameScenes.globalPoolManager.DespawnWeapon(gameObject, weaponSO.weaponType);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isContact = true;
            EnemyObj = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isContact = false;
        }
    }
}
