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
    [System.Serializable]
    public class WeaponPool
    {
        public ObjectType.WeaponType weaponType;
        public GameObject prefab;
        public int initialSize;
    }

    [SerializeField] public List<MonsterPool> monsterPools;
    [SerializeField] public List<WeaponPool> weaponPools;
    [SerializeField] private Transform monsterParentTransform;
    [SerializeField] private Transform weaponParentTransform;
    private Dictionary<ObjectType.MonsterType, Queue<GameObject>> poolDictionary;
    private Dictionary<ObjectType.WeaponType, Queue<GameObject>> weaponPoolDictionary;
    private GameObject[] monsterToSpawn;
    private GameObject weaponToSpawn;


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

        weaponPoolDictionary = new Dictionary<ObjectType.WeaponType, Queue<GameObject>>();

        foreach(var weapon in weaponPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < weapon.initialSize; i++)
            {
                GameObject obj = Instantiate(weapon.prefab, weaponParentTransform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            weaponPoolDictionary.Add(weapon.weaponType, objectPool);
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

    public GameObject SpawnWeapon(ObjectType.WeaponType type)
    {
        if (!weaponPoolDictionary.ContainsKey(type))
        {
            Debug.LogError("No pool with such type!");
            return null;
        }
        
        weaponToSpawn = weaponPoolDictionary[type].Count > 0 ? weaponPoolDictionary[type].Dequeue() : Instantiate(GetPrefabByType(type));
        weaponToSpawn.SetActive(true);

        return weaponToSpawn;
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

    private GameObject GetPrefabByType(ObjectType.WeaponType type)
    {
        foreach (var pool in weaponPools)
        {
            if (pool.weaponType == type)
            {
                return pool.prefab;
            }
        }
        return null;
    }

    private Vector3 RandomPosition(Vector3 playerPosition, float distance)
    {
        var rCircleX = Random.insideUnitCircle.x * distance;
        var rCircleY = Random.insideUnitCircle.y * distance;
   
        Vector3 randomDirection = new Vector3(Mathf.Clamp(rCircleX, distance * 0.7f, distance), Mathf.Clamp(rCircleY, distance * 0.7f, distance), 0);
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

    public void DespawnWeapon(GameObject weapon, ObjectType.WeaponType type)
    {
        weapon.SetActive(false);
        if (weaponPoolDictionary.ContainsKey(type))
        {
            weaponPoolDictionary[type].Enqueue(weapon);
        }
        else
        {
            Destroy(weapon);
        }
    }
}
