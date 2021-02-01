using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject potion, axeItem, gunItem;
    // Start is called before the first frame update
    void Start()
    {
        spawnItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnItem()
    {
        Instantiate(potion, spawnPoints[Random.Range(0, 4)].position, Quaternion.identity);
        if (!PlayerPrefs.HasKey("axeGet"))
        {
            Instantiate(axeItem, spawnPoints[4].position, Quaternion.identity);
        }
        if (!PlayerPrefs.HasKey("gunGet"))
        {
            Instantiate(gunItem, spawnPoints[5].position, Quaternion.identity);
        }
    }
}
