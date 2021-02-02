using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour
{
    private float amplitude = 0.2f;
    public float frequency = 1f;
    public AudioClip pickUp;
    public PlayerMove playerMove;
    public GameObject textHolder;
    public Text textInfo;

    GameObject textInfoObject;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Awake()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        textHolder = GameObject.Find("textHolder");
        textInfoObject = GameObject.Find("textInfo");
        if(textInfoObject != null) textInfo = textInfoObject.GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(textHolder != null) textHolder.SetActive(false);
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

            if (gameObject.name == "SpeedPotion(Clone)")
            {
                playerMove.moveSpeed = 5;
                playerMove.timeElapsed = 0;
                SoundManager.soundManager.speedSound();
                Destroy(gameObject);
                return;
            }
            else if (gameObject.name == "Axe(Clone)")
            {
                textHolder.SetActive(false);
                PlayerPrefs.SetInt("axeGet",1);
                textInfo.text = "Axe Unlocked";
            }
            else if (gameObject.name == "Gun(Clone)")
            {
                textHolder.SetActive(false);
                PlayerPrefs.SetInt("gunGet", 1);
                textInfo.text = "Gun Unlocked";
            }
            else if (gameObject.name == "Map(Clone)")
            {
                textHolder.SetActive(false);
                PlayerPrefs.SetInt("mapGet", 1);
                GameManager.gameManager.minimap.SetActive(true);
                textInfo.text = "Minimap Unlocked";
            }
            textHolder.SetActive(true);
            SoundManager.soundManager.itemSound();
            Destroy(gameObject);
        }
    }
}
