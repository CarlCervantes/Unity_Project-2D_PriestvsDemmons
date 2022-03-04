using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static int highScore = 100;
    public  int totalCoins;

    public Text scoreText;
    public Text highScoreText;

    public Invaders invaders;
    public Shields shields;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
    }

    private void Awake()
    {
        invaders = FindObjectOfType<Invaders>();
    }

    // Update is called once per frame
    void Update()
    {
        SetScoreText(totalCoins);

        if (totalCoins > highScore)
        {
            SetHighScoreText(totalCoins);
        }

        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if (invaders.amountAlive == 0)
        {
            shields.numberOfShields++;
            NewRound();
        }
    }

    public void Play_Replay()
    {
        SceneManager.LoadScene("Game_Beta");
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

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
    public void SetHighScoreText(int score)
    {
        highScoreText.text = score.ToString();
    }

    private void NewRound()
    {
        invaders.resetInvaders();
        invaders.gameObject.SetActive(true);
    }
}
