using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer playerGraf;
    public Sprite[] playerIdle;
    private PlayerMove playerMove;

    enum weaponType { None, Axe, Smg};
    private weaponType currentWeapon;

    void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = weaponType.None;
        anim.SetBool("idleNormal", true);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    //void Attack()
    //{
    //    audioSource.PlayOneShot(attack);
    //    anim.SetTrigger("Attack");
    //}
}
