using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shields : MonoBehaviour
{
    public int health;
    public int numberOfShields;
    public Image[] hits;
    public Sprite shields;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health > numberOfShields)
        {
            health = numberOfShields;
        }
        for (int i=0; i < hits.Length; i++)
        {
            if (i < numberOfShields)
            {
                hits[i].enabled = true;
            }
            else
            {
                hits[i].enabled = false;
            }
        }

        if(numberOfShields < 0)
        {
            GameManager.gameOver = true;
        }
    }
}
