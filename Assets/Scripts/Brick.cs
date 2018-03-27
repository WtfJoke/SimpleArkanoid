﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public int lives = 1;


    public void Hit()
    {
        lives -= 1;
        GameManager.instance.Points++;

        if (lives <= 0)
        {
            gameObject.SetActive(false);
            GameManager.instance.ReduceBrick();
        }
        else
        {
            CycleBrickColor();
        }
    }

    private void CycleBrickColor()
    {
        Color colorToSet;
        switch (lives)
        {
            case 1:
                colorToSet = Color.blue;
                break;
            case 2:
                colorToSet = Color.red;
                break;
            case 3:
                colorToSet = Color.yellow;
                break;
            default:
                colorToSet = Color.black;
                break;
        }

        GetComponent<Renderer>().material.color = colorToSet; // assign color
    }
}
