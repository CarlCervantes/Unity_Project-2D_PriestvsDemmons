using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EventMENU : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play_Replay()
    {
        SceneManager.LoadScene("GAME");
    }
    public void Controls()
    {
        SceneManager.LoadScene("Controles");
    }
    public void Return()
    {
        SceneManager.LoadScene("MENU");
    }
    public void closeGame()
    {
        Application.Quit();
    }
}
