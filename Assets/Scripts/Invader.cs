using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public System.Action killed;
    public Sprite[] animsprites;
    public float animTime = 0.8f;
    public GameManager manager;
    private SpriteRenderer render;

    private int animationFrame;

    // Start is called before the first frame update
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        InvokeRepeating(nameof(animateSprite), this.animTime, this.animTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void animateSprite()
    {
        animationFrame++;
        if (animationFrame >= this.animsprites.Length)
        {
            animationFrame = 0;
        }

        render.sprite = this.animsprites[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player_Projectile"))
        {
            
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }

    }
}
