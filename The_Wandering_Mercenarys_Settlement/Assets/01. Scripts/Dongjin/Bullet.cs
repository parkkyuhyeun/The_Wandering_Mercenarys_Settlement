using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Bullet : MonoBehaviour
{
    private void Awake()
    {
        GameScenes.globalBullet = this;
    }

    public IEnumerator ShootAttack(EnemySO enemySO, GameObject hand, GameObject player)
    {
        var bullet = GameScenes.globalPoolManager.SpawnWeapon(enemySO.weaponType);
        bullet.transform.position = hand.transform.position;
        var bulletCol = bullet.GetComponent<Collider2D>();
        float timer = 0;

        // 플레이어와 적 사이의 방향을 계산
        Vector2 direction = (player.transform.position - bullet.transform.position).normalized;

        // 적의 회전을 플레이어를 향하도록 설정
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
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
            bullet.transform.Translate(Vector2.right * enemySO.bulletSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        Debug.Log("총알 사라짐");
        GameScenes.globalPoolManager.DespawnWeapon(bullet, enemySO.weaponType);
        yield return null;
    }
}
