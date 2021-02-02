using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    public AudioClip raceMusic, axeAttack, shootAttack, itemPickup, speedPickup, winGame, gameOver, deathEnemy;
    public AudioSource cameraSource, player;
    // Start is called before the first frame update
    void Start()
    {
        if (soundManager == null)
            soundManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeMusic()
    {
        cameraSource.clip = raceMusic;
        cameraSource.Play();
    }

    public void itemSound()
    {
        cameraSource.PlayOneShot(itemPickup);
    }

    public void speedSound()
    {
        cameraSource.PlayOneShot(speedPickup);
    }

    public void deathSound()
    {
        cameraSource.PlayOneShot(deathEnemy);
    }

    public void axeSound()
    {
        player.PlayOneShot(axeAttack);
    }

    public void shootSound()
    {
        player.PlayOneShot(shootAttack);
    }

    public void winSound()
    {
        cameraSource.clip = null;
        cameraSource.PlayOneShot(winGame);
    }

    public void loseSound()
    {
        cameraSource.clip = null;
        cameraSource.PlayOneShot(gameOver);
    }

}
