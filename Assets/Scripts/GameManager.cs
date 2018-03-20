using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int remainingBricks;

	// Use this for initialization
	void Start () {
        remainingBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
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
