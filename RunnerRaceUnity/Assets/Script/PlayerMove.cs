using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public Transform playerGraf;

    private AudioSource audioSource;
    [HideInInspector]
    public Vector2 movement;

    private float firstMoveSpeed;

    [HideInInspector]
    public float timeElapsed;
    public float lerpDuration = 2;

    float startValue;
    float endValue;
    float valueToLerp;

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        timeElapsed = 0;
        firstMoveSpeed = endValue = moveSpeed;
        startValue = moveSpeed + 3;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
        if(moveSpeed != firstMoveSpeed)
        {
            lerpSpeed();
        }
    }

    void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //if (movement != Vector2.zero) anim.SetBool("Run", true);
        if (movement.y > 0.01f) playerGraf.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        else if (movement.y < -0.01f) playerGraf.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        else if (movement.x > 0.01f) playerGraf.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
        else if (movement.x < -0.01f) playerGraf.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
    }

    void lerpSpeed()
    {
        {
            if (timeElapsed < lerpDuration)
            {
                moveSpeed = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                moveSpeed = endValue;
            }
        }
    }
}
