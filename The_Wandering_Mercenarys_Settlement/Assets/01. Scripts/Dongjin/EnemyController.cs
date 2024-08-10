using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();

    [SerializeField] private int maxHP;
    [SerializeField] private int curHP;


    private void Awake()
    {
        GameScenes.globalEnemyController = this;  
    }

    void Start()
    {
        foreach(var monsterPool in GameScenes.globalPoolManager.monsterPools)
        {
            enemys.Add(monsterPool.prefab);
        }
    }

    void Update()
    {
        //юс╫ц
        if(Input.GetKey(KeyCode.M))
            UpdateAllEnemys();
    }

    public void UpdateAllEnemys()
    {
        enemys.Clear();
        foreach (var monsterPool in GameScenes.globalPoolManager.monsterPools)
        {
            enemys.Add(monsterPool.prefab);
        }
    }
}
