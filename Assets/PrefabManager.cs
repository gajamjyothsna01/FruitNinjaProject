using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // An array of fruit Prefabs
    public GameObject[] fruitPrefabs;

    #region SINGLETON METHOD
    private static PrefabManager instance;
    public static PrefabManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PrefabManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("PrefabManager");
                    container.AddComponent<PrefabManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #region PUBLIC METHODS
    // Return a large asteroid prefab.
    public GameObject GetFruitPrefabs()
    {
        if (fruitPrefabs.Length > 0)
            return fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

        return null;
    }
    #endregion
}
