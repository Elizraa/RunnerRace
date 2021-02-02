using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public GameObject chest, minimap, startButton, resetButton, endPanel;
    public Text winOrLose;
    public List<Dropdown.OptionData> weapon = new List<Dropdown.OptionData>();
    public Dropdown dropdownWeapon;

    public enum gameState { menu, start, end};
    public gameState state;


    void Awake()
    {
        if (gameManager == null)
            gameManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        state = gameState.menu;
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
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void StartRace()
    {
        state = gameState.start;
        chest.SetActive(false);
        startButton.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RetryRace()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        RetryRace();
    }

    public void raceDone(string tag)
    {
        if (tag == "Player")
        {
            PlayerPrefs.SetInt("winner", 1);
        }
        else if (tag == "Enemy")
        {
            int random = Random.Range(0, 2);
            if (random == 1) winOrLose.text = "Someone already on the finish line... try again?";
            else winOrLose.text = "Need to go faster to the finish line, lets try again";
        }
        else return;
        state = gameState.end;
        endPanel.SetActive(true);
        if (PlayerPrefs.GetInt("winner", 0) == 0) resetButton.SetActive(false);
        Time.timeScale = 0f;
    }
}
