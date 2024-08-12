using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();

    [Header("Ω∫≈»")]
    [SerializeField] private EnemySO enemySO;

    private float curHP;


    private void Awake()
    {
        GameScenes.globalEnemyController = this;  
    }

    void Start()
    {
        curHP = enemySO.MaxHP;
    }

    void Update()
    {
        
    }

    private void FindPlayerAndFollow()
    {

    }

    public void TakeDamage(float Damage)
    {
        curHP -= Damage;
        if(curHP < 0)
        {
            GameScenes.globalPoolManager.DespawnMonster(gameObject, enemySO.monsterType);
        }
    }
}
