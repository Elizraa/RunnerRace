using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer playerGraf;
    public Sprite[] playerIdle;
    private PlayerMove playerMove;
    private bool attacking;
    public float waitForNextAttack;
    enum weaponType { None, Axe, Smg};
    private weaponType currentWeapon;

    public Transform shootPoint;
    public GameObject bullet;
    public float shootDistanceInSeconds = 1f;

    void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }
    // Start is called before the first frame update
    void Start()
    {
        int i = PlayerPrefs.GetInt("currentWeapon");
        switch (i)
        {
            case 0:
                currentWeapon = weaponType.None;
                anim.SetBool("idleNormal", true);
                break;
            case 1:
                currentWeapon = weaponType.Axe;
                anim.SetBool("idleAxe", true);
                break;
            case 2:
                currentWeapon = weaponType.Smg;
                anim.SetBool("idleSmg", true);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentWeapon != weaponType.None && !attacking && GameManager.gameManager.state == GameManager.gameState.start) Attack();
    }

    private void FixedUpdate()
    {
        runAnimation();
    }

    void runAnimation()
    {
        if (playerMove.movement == Vector2.zero)
        {
            if (currentWeapon == weaponType.None)
            {
                anim.SetBool("runNormal", false);
            }
            else if (currentWeapon == weaponType.Axe)
            {
                anim.SetBool("runAxe", false);
            }
            else if (currentWeapon == weaponType.Smg)
            {
                anim.SetBool("runSmg", false);
            }
            return;
        }

        if (currentWeapon == weaponType.None)
        {
            anim.SetBool("runNormal", true);
        }
        else if(currentWeapon == weaponType.Axe)
        {
            anim.SetBool("runAxe", true);
        }
        else if(currentWeapon == weaponType.Smg)
        {
            anim.SetBool("runSmg", true);
        }
    }
    void Attack()
    {
        attacking = true;
        //audioSource.PlayOneShot(attack);
        if (currentWeapon == weaponType.Axe)
        {
            anim.SetTrigger("attackAxe");
            SoundManager.soundManager.axeSound();
        }
        else if (currentWeapon == weaponType.Smg)
        {
            anim.SetTrigger("attackSmg");
            SoundManager.soundManager.shootSound();
            Shoot();
        }
        attacking = false;
    }

    void Shoot()
    {
        GameObject bulletInstantiate = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }

    public void changeWeapon(int value)
    {
        switch (value)
        {
            case 0:
                currentWeapon = weaponType.None;
                anim.SetBool("idleNormal", true);
                anim.SetBool("idleAxe", false);
                anim.SetBool("idleSmg", false);

                break;
            case 1:
                currentWeapon = weaponType.Axe;
                anim.SetBool("idleNormal", false);
                anim.SetBool("idleAxe", true);
                anim.SetBool("idleSmg", false);
                break;
            case 2:
                currentWeapon = weaponType.Smg;
                anim.SetBool("idleNormal", false);
                anim.SetBool("idleAxe", false);
                anim.SetBool("idleSmg", true);
                break;
        }
        PlayerPrefs.SetInt("currentWeapon", value);
    }
}
