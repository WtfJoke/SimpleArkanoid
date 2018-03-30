using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public int remainingBricks;
    public Text pointsText;
    public Text lifeText;
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
                RestartGame();
            }
        }
    }
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
            // This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);



        //Call the InitGame function to initialize the first level 
        InitGame();
    }
    

    public void ReduceBrick()
    {
        remainingBricks -= 1;
        if (remainingBricks == 0)
        {
            RestartGame();
        }
    }

    private void InitGame()
    {
        bricks = GameObject.FindGameObjectsWithTag("Brick");
        StartGame();
    }

    private void StartGame()
    {
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
