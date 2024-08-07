using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class MonsterPool
    {
        public ObjectType.MonsterType monsterType;
        public GameObject prefab;
        public int initialSize;
    }

    [SerializeField] public List<MonsterPool> monsterPools;
    [SerializeField] private Transform monsterParentTransform;
    private Dictionary<ObjectType.MonsterType, Queue<GameObject>> poolDictionary;
    private GameObject[] monsterToSpawn;


    private void Awake()
    {
        GameScenes.globalPoolManager = this;
        InitializePools();
    }

    void Start()
    {
        
    }

    private void InitializePools()
    {
        poolDictionary = new Dictionary<ObjectType.MonsterType, Queue<GameObject>>();

        foreach (var pool in monsterPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.initialSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, monsterParentTransform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.monsterType, objectPool);
        }
    }

    public GameObject[] SpawnMonster(ObjectType.MonsterType type, float distance, Vector3 playerPosition, int spawnCount)
    {
        monsterToSpawn = new GameObject[spawnCount];
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogError("No pool with such type!");
            return null;
        }
        for(int i = 0; i < spawnCount; ++i)
        {
            monsterToSpawn[i] = poolDictionary[type].Count > 0 ? poolDictionary[type].Dequeue() : Instantiate(GetPrefabByType(type));
            monsterToSpawn[i].SetActive(true);

            Vector3 spawnPosition = RandomPosition(playerPosition, distance);
            monsterToSpawn[i].transform.position = spawnPosition;
        }

        return monsterToSpawn;
    }

    private GameObject GetPrefabByType(ObjectType.MonsterType type)
    {
        foreach (var pool in monsterPools)
        {
            if (pool.monsterType == type)
            {
                return pool.prefab;
            }
        }
        return null;
    }

    private Vector3 RandomPosition(Vector3 playerPosition, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += playerPosition;
        return new Vector3(randomDirection.x, randomDirection.y, 0);
    }

    public void DespawnMonster(GameObject monster, ObjectType.MonsterType type)
    {
        monster.SetActive(false);
        if (poolDictionary.ContainsKey(type))
        {
            poolDictionary[type].Enqueue(monster);
        }
        else
        {
            Destroy(monster);
        }
    }
}
