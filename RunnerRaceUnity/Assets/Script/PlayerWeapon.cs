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

    public float damageAxe = 10;

    void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = weaponType.Smg;
        anim.SetBool("idleSmg", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentWeapon != weaponType.None && !attacking) Attack();
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
        }
        else if (currentWeapon == weaponType.Smg)
        {
            anim.SetTrigger("attackSmg");
            Shoot();
        }
        attacking = false;
    }

    void Shoot()
    {
        GameObject bulletInstantiate = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }
}
