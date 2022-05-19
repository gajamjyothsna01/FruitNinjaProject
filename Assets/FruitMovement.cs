using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitMovement : MonoBehaviour
{
    public float speed = 20f;

    #region PRIVATE VARIABLES
   private SpriteRenderer spriteRenderer;
   private PolygonCollider2D polyCollider;
    Rigidbody2D rigidbody2D;
    #endregion

   void Awake()
   {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        polyCollider = (PolygonCollider2D)GetComponent<Collider2D>();

    }
    #region MONOBEHAVIOUR METHODS
    
    void OnEnable()
    {
        ResetFromPrefab();
        ApplyForce();
       Debug.Log("ApplyForce");
    }
    public void ApplyForce()
    {

        rigidbody2D.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
        Debug.Log("Force Applied");
    }
    

    void OnDisable()
    {
        rigidbody2D.angularVelocity = 0f;
        rigidbody2D.velocity = Vector2.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
   
    private void ResetFromPrefab()
    {
             GameObject prefab = PrefabManager.Instance.GetFruitPrefabs();
            spriteRenderer.sprite = prefab.GetComponentInChildren<SpriteRenderer>().sprite;

            PolygonCollider2D prefabCollider = ((PolygonCollider2D)prefab.GetComponentInChildren<Collider2D>());
            polyCollider.pathCount = prefabCollider.pathCount;

            for (int i = 0; i < prefabCollider.pathCount; i++)
                polyCollider.SetPath(i, prefabCollider.GetPath(i));
        
        
    }
    #endregion
}
