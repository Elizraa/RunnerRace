using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public float speed = 35f;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    private Tilemap tilemap;

    public int damageBullet = 15;
    
    void Awake()
    {
        tilemap = GameObject.Find("Wall").GetComponent<Tilemap>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D hit)
    {
        if (hit.CompareTag("Item")) return;
        if (hit.CompareTag("Enemy"))
        {
            hit.GetComponent<EnemyControl>().takeDamage(damageBullet);
            StartCoroutine(Impact(transform.position));
        }
        rb.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;
        if (collision.gameObject.CompareTag("Wall"))
        {
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                StartCoroutine(Impact(hitPosition));
            }
        }
    }

    IEnumerator Impact(Vector2 impactHere)
    {
        GameObject prefabEffect = Instantiate(impactEffect, impactHere, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Destroy(prefabEffect);
    }
}
