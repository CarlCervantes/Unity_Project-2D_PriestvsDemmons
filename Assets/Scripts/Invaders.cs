using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    //el escript de animacion sera nuestro arreglo
    public  Invader [] prefabs;
    public GameManager manager;

    public int rows = 10 ;

    public int columns = 5;
    public AnimationCurve speed;
    public float attackRate = 1.0f;
    

    private Vector3 _direction = Vector2.down;
    public Vector3 initialPosition { get; private set; }

    public Projectile eProjectilePrefab;

    public int amountKilled { get; private set; }

    public int amountAlive => this.totalInvaders - this.amountKilled; 

    public int totalInvaders => this.rows * this.columns;
    public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders;

    private void Awake()
    {
        initialPosition = transform.position;
        for (int row = 0; row < this.rows; row++)
        {
            float width = 3.0f * (this.rows - 1);
            float height = 3.0f * (this.columns - 1);
            Vector3 centering = new Vector2(-width / 2, -height/ 2) ;
            Vector3 rowPosition = new Vector3(centering.x + (row * 3.0f), centering.y, 0.0f);

            for(int col = 0; col < this.columns; col++)
            {
                // Create an invader and parent it to this transform
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;

                // Calculate and set the position of the invader in the row
                Vector3 position = rowPosition;
                position.y += col * 3.0f;
                invader.transform.localPosition = position;
            }
        }
    }
    private void Start()
    {
        InvokeRepeating(nameof(Attack), this.attackRate, this.attackRate);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        Vector3 downEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 upEdge = Camera.main.ViewportToWorldPoint(Vector3.up);

        foreach(Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (_direction == Vector3.up && invader.position.y >= (upEdge.y -1.0f))
            {
                AdvanceRow();
            }
            else if(_direction == Vector3.down && invader.position.y <= (downEdge.y + 1.0f))
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        _direction.y *= -1.0f;

        Vector3 position = this.transform.position;
        position.x -= 1.0f;
        this.transform.position = position;
    }

    private void InvaderKilled()
    {
        this.amountKilled++;
        manager.totalCoins += 10;
       
    }

    private void Attack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1.0f / (float) this.amountAlive))
            {
                Instantiate(this.eProjectilePrefab, invader.position, Quaternion.identity);
                break;
                //el break garantiza que solo saldra un ataque en lugar de varios a la vez
            }
        }
    }

    public void resetInvaders()
    {
        amountKilled = 0;
        _direction = Vector2.down;
        transform.position = initialPosition;

        foreach(Transform invader in transform)
        {
            invader.gameObject.SetActive(true);
        }
    }
}
