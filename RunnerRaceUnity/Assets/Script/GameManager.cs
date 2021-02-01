using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public GameObject chest, minimap;
    public List<Dropdown.OptionData> weapon = new List<Dropdown.OptionData>();
    public Dropdown dropdownWeapon;

    void Awake()
    {
        if (gameManager == null)
            gameManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        dropdownWeapon.ClearOptions();
        if (PlayerPrefs.HasKey("mapGet"))
        {
            minimap.SetActive(true);
        }
        if (PlayerPrefs.HasKey("gunGet"))
        {
            chest.SetActive(true);
            dropdownWeapon.AddOptions(weapon);
        }
        else if (PlayerPrefs.HasKey("axeGet"))
        {
            chest.SetActive(true);
            dropdownWeapon.AddOptions(new List<Dropdown.OptionData>(){ weapon[0],weapon[1]});
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
