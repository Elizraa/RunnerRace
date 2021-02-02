using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public GameObject chest, minimap, startButton, resetButton, endPanel, title;
    public Text winOrLose, countdownText;
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
        chest.SetActive(false);
        title.SetActive(false);
        startButton.SetActive(false);
        StartCoroutine(countDown());
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
            SoundManager.soundManager.winSound();
        }
        else if (tag == "Enemy")
        {
            int random = Random.Range(0, 2);
            if (random == 1) winOrLose.text = "Someone already on the finish line... try again?";
            else winOrLose.text = "Need to go faster to the finish line, lets try again";
            SoundManager.soundManager.loseSound();
        }
        else return;
        state = gameState.end;
        endPanel.SetActive(true);
        if (PlayerPrefs.GetInt("winner", 0) == 0) resetButton.SetActive(false);
        Time.timeScale = 0f;
    }

    IEnumerator countDown()
    {
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(0.5f);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(0.5f);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(0.5f);
        countdownText.text = "";
        state = gameState.start;
        SoundManager.soundManager.changeMusic();
        Time.timeScale = 1f;
    }
}
