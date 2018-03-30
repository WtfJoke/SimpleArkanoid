using System;
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
    public int activeBalls = 1;
    public int spawnPowerUpEvery = 4;
    public PowerUp enlargePrefab;
    public PowerUp shrinkPrefab;
    public PowerUp twoBallsPrefab;

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
            Won();
        }
    }

    public void SpawnPowerUp(Transform spawnPos)
    {
        if (remainingBricks % spawnPowerUpEvery == 0)
        {
            int powerupCount = Enum.GetNames(typeof(PowerUp.PowerUpType)).Length;
            int random = UnityEngine.Random.Range(0, powerupCount);
            switch (random)
            {
                case 0:
                    Instantiate<PowerUp>(enlargePrefab, spawnPos.position, spawnPos.rotation);
                    break;
                case 1:
                    Instantiate<PowerUp>(shrinkPrefab, spawnPos.position, spawnPos.rotation);
                    break;
                case 2:
                    Instantiate<PowerUp>(twoBallsPrefab, spawnPos.position, spawnPos.rotation);
                    break;
                default:
                    break;
            }

        }
    }

    public void SpawnBall()
    {
        activeBalls++;
        Ball ball = GameObject.FindObjectOfType<Ball>();


        Ball newBall = Instantiate<Ball>(ball);
        newBall.Rotate(ball.getVelocity());

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
        GameObject.FindObjectOfType<Ball>().Respawn();
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
