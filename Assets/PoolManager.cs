using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectToPool
{
    public GameObject prefab;
    public int initialCapacity;
}

public class PoolManager : MonoBehaviour
{
    #region PUBLIC VARIABLES
    // Objects to be pooled at initialization.
    public ObjectToPool[] prefabsToPool;
    #endregion

    #region PRIVATE VARIABLES
    private Dictionary<string, ObjectPool> pools;
    #endregion

    #region SINGLETON METHOD
    private static PoolManager instance;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PoolManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("PoolManager");
                    container.AddComponent<PoolManager>();
                }
            }
            return instance;
        }
    }
    #endregion


    #region MONOBEHAVIOUR METHODS
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < prefabsToPool.Length; i++)
        {
            CreatePool(prefabsToPool[i].prefab, prefabsToPool[i].initialCapacity);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region PUBLIC METHODS
    public void CreatePool(GameObject prefab, int initialCapacity)
    {
        if (pools == null)
            pools = new Dictionary<string, ObjectPool>();

        ObjectPool newPool = new ObjectPool(prefab, initialCapacity);
        pools.Add(prefab.name, newPool);
    }

    // Spawn an object with the given name.
    public GameObject Spawn(string prefabName)
    {

        if (!pools.ContainsKey(prefabName)) //if there no name, then return null
            return null;

        return pools[prefabName].Spawn();
    }

    // Recycle an object with the given name.
    public void Recycle(string prefabName, GameObject obj)
    {
        if (!pools.ContainsKey(prefabName))
            return;

        pools[prefabName].Recycle(obj);
    }
    #endregion

}
