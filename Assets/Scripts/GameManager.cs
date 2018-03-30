using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public int remainingBricks;
    public Text pointsText;
    public Text lifeText;
    public Text gameOverText;
    public Text youWonText;
    public Text restartText;
    public float finishTextDisplayTime = 0.1f;
    public int Points
    {
        get { return _points; }
        set
        {
            _points = value;
            pointsText.text = "Points: " + Points;
        }
    }
    public int Lifes
    {
        get { return _lifes; }
        set
        {
            _lifes = value;
            string hearts = "";
            for (int i = 0; i < _lifes; i++)
            {
                hearts += "♡";
            }
            lifeText.text = hearts;
            if (_lifes == 0)
            {
                GameOver();
            }
        }
    }

    private bool isRunning;
    private int _points;
    private int _lifes;
    private GameObject[] bricks;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        InitGame();
    }

    public void Update()
    {
        if (!isRunning && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();   
        }
    }


    public void ReduceBrick()
    {
        remainingBricks -= 1;
        if (remainingBricks == 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        isRunning = false;
    }

    private void Won()
    {
        youWonText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        isRunning = false;
    }

    private void InitGame()
    {
        bricks = GameObject.FindGameObjectsWithTag("Brick");
        StartGame();
    }

    private void StartGame()
    {
        isRunning = true;
        youWonText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);

        foreach (var brickObj in bricks)
        {
            Brick brick = brickObj.GetComponent<Brick>();
            if (brick.NeedsReset())
            {
                brick.ResetState();
            }

        }
        remainingBricks = bricks.Length;
        Points = 0;
        Lifes = 3;
    }

    private void RestartGame()
    {
        StartGame();
        GameObject.FindObjectOfType<Ball>().Respawn();
    }
}
