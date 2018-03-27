using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public int remainingBricks;
    public Text pointsText;
    public int Points
    {
        get { return _points; }
        set
        {
            _points = value;
            pointsText.text = "Points: " + Points;
        }
    }
    private int _points;

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
        // InitGame();
    }

   

    // Use this for initialization
    void Start () {
        remainingBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        Points = 0;
    }
	
	// Update is called once per frame
	void Update () {
		// TODO restart game on remainingBricks == 0
	}

    public void ReduceBrick()
    {
        remainingBricks -= 1;
    }
}
