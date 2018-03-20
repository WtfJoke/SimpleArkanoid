using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int remainingBricks;
    public Text pointsText;
    public Text lifesText;
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
            SetLifeText();
        }
    }

    private int _lifes;
    private int _points;

    // Use this for initialization
    void Start () {
        remainingBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        Points = 0;
        Lifes = 3;
    }
	
	// Update is called once per frame
	void Update () {
		// TODO restart game on remainingBricks == 0
	}

    public void ReduceBrick()
    {
        remainingBricks -= 1;
        Points++;
    }

    private void SetLifeText()
    {
        string livecount = "";
        for (int i = 0; i < _lifes; i++)
        {
            livecount += "o";
        }
        lifesText.text = livecount;
    }
}
