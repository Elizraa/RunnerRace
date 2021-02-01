using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    private float amplitude = 0.2f;
    public float frequency = 1f;
    public AudioClip pickUp;
    public PlayerMove playerMove;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Awake()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }
    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(("Player")))
        {
            if(gameObject.name == "SpeedPotion(Clone)")
            {
                playerMove.moveSpeed = 5;
                playerMove.timeElapsed = 0;
                Destroy(gameObject);
            }
            else if (gameObject.name == "Axe(Clone)")
            {
                PlayerPrefs.SetInt("axeGet",1);
                Destroy(gameObject);
            }
            else if (gameObject.name == "Gun(Clone)")
            {
                PlayerPrefs.SetInt("gunGet", 1);
                Destroy(gameObject);
            }
            else if (gameObject.name == "Map(Clone)")
            {
                PlayerPrefs.SetInt("mapGet", 1);
                GameManager.gameManager.minimap.SetActive(true);
                Destroy(gameObject);
            }
            Debug.Log(gameObject.name);
        }
    }
}
