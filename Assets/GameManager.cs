using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON REGION
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<GameManager>();
                }
            }
            return instance;
        }

    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFruits());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator SpawnFruits()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 8f));
            SpawnFruit();
        }
    }

    private void SpawnFruit()
    {
        FruitMovement newFruit = PoolManager.Instance.Spawn("Fruits").GetComponent<FruitMovement>();
        newFruit.transform.position = this.transform.position;
        SpriteRenderer spriteRenderer = newFruit.GetComponentInChildren<SpriteRenderer>();
    }
}
