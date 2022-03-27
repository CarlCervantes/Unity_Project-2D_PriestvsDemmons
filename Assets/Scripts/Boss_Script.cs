using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Script : MonoBehaviour
{
    public float speed = 5f;
    public float cycleTime = 30f;
    
    private Animator animator;
    public System.Action<Boss_Script> killed;
    public GameManager manager;
    public Vector3 upDestination { get; private set; }
    public Vector3 downDestination { get; private set; }

    public int direction { get; private set; } = -1;
    public bool spawned { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Vector3 downEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 upEdge = Camera.main.ViewportToWorldPoint(Vector3.up);

        Vector3 down = transform.position;

        down.y = downEdge.y - 1f;
        downDestination = down;

        Vector3 up = transform.position;

        up.y = upEdge.y + 1f;
        upDestination = up;

        transform.position = downDestination;
        Despawn();

    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            return;
        }

        if (direction == 1)
        {
            moveUp();
        }
        else
        {
            moveDown();
        }
    }

    private void moveUp()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if (transform.position.y >= upDestination.y)
        {
            Despawn();
        }
    }

    private void moveDown()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (transform.position.y <= downDestination.y)
        {
            Despawn();
        }
    }

    private void Spawn()
    {
        direction *= -1;

        if(direction == 1)
        {
            transform.position = downDestination;
        }
        else
        {
            transform.position = upDestination;
        }

        spawned = true;
    }

    private void Despawn()
    {
        spawned = false;

        if(direction == 1)
        {
            transform.position = upDestination;
        }
        else
        {
            transform.position = downDestination;
        }

        Invoke(nameof(Spawn), cycleTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player_Projectile"))
        {
            manager.totalCoins += 500;
            Despawn();
            if (killed != null)
            {
                killed.Invoke(this);
            }
        }
    }
}
