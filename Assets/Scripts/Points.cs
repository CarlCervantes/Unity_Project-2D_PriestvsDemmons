using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int points = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Easy")
        {
            points = 10;
        }
        else if(collision.tag == "Medium")
        {
            points = 25;
        }
        else if(collision.tag == "Hard")
        {
            points = 50;
        }
        else
        {
            Destroy(this);
        }
    }
}
