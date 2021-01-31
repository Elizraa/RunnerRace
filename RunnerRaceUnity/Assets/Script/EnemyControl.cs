using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float health;
    public float speed;
    public Transform[] turnPoint;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator anim;

    enum turn {Zero, First, Second, Third, Fourth, Fifth, Sixth, Seventh };
    private turn currentTurn;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("run", true);
        movement = new Vector2(-1, 0);
        currentTurn = turn.Zero;
    }

    private void Update()
    {
        if(transform.position.x <= turnPoint[0].position.x && currentTurn == turn.Zero)
        {
            transform.rotation = turnPoint[0].rotation;
            movement = new Vector2(0, -1);
            currentTurn = turn.First;
        }
        else if (transform.position.y <= turnPoint[1].position.y && currentTurn == turn.First)
        {
            transform.rotation = turnPoint[1].rotation;
            movement = new Vector2(1, 0);
            currentTurn = turn.Second;
        }
        else if (transform.position.x >= turnPoint[2].position.x && currentTurn == turn.Second)
        {
            transform.rotation = turnPoint[2].rotation;
            movement = new Vector2(0, -1);
            currentTurn = turn.Third;
        }
        else if (transform.position.y <= turnPoint[3].position.y && currentTurn == turn.Third)
        {
            transform.rotation = turnPoint[3].rotation;
            movement = new Vector2(1, 0);
            currentTurn = turn.Fourth;
        }
        else if (transform.position.x >= turnPoint[4].position.x && currentTurn == turn.Fourth)
        {
            transform.rotation = turnPoint[4].rotation;
            movement = new Vector2(0, 1);
            currentTurn = turn.Fifth;
        }
        else if (transform.position.y >= turnPoint[5].position.y && currentTurn == turn.Fifth)
        {
            transform.rotation = turnPoint[5].rotation;
            movement = new Vector2(1, 0);
            currentTurn = turn.Sixth;
        }
        else if (transform.position.x >= turnPoint[6].position.x && currentTurn == turn.Sixth)
        {
            transform.rotation = turnPoint[6].rotation;
            movement = new Vector2(0, 1);
            currentTurn = turn.Seventh;
        }
    }

    void FixedUpdate()
    {
        move();
    }

    void move()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);   
    }
}
