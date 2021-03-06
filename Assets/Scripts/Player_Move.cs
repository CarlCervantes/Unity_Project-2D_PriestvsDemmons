using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed = 5.0f;

    public Projectile projectilePrefab;
    public Shields hits;
    private Animator animator;

    private bool isFireActive;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            this.transform.position += Vector3.up * this.speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            this.transform.position += Vector3.down * this.speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
             
        }

        
    }
    void Shooting()
    {
        if (!isFireActive)
        {
            Projectile projectile = Instantiate(this.projectilePrefab, this.transform.position, Quaternion.identity);
            //projectile.destroyed += fireDestroy;
            //isFireActive = true;
        }
        
    }

    public IEnumerator Attack()
    {
        animator.SetBool("Attacking", true);
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.33f);
        //yield return new WaitForSeconds(.1f);
        Shooting();
    }

    

    void fireDestroy()
    {
        isFireActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy_Projectile"))
        {
            animator.SetTrigger("TakeDamage");
            hits.numberOfShields--;
          
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            GameManager.gameOver = true;
        }
    }
}
