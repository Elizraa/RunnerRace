using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public float speed = 35f;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public float distanceInSeconds = 0.2f;

    private bool hitSomething;
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
        StartCoroutine(destroyBullet());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item")) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyControl>().takeDamage(damageBullet);
            StartCoroutine(Impact(transform.position));
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                StartCoroutine(Impact(hitPosition));
            }
        }
        rb.velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().enabled = false;
    }


    IEnumerator Impact(Vector2 impactHere)
    {
        hitSomething = true;
        GameObject prefabEffect = Instantiate(impactEffect, impactHere, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        Destroy(prefabEffect);
        Destroy(gameObject);
    }

    IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(distanceInSeconds);
        if (hitSomething) yield break;
        Destroy(gameObject);
    }
}
