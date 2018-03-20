using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    public void Hit()
    {
        gameObject.SetActive(false);
        GameObject.FindObjectOfType<GameManager>().ReduceBrick();
        // TODO: add point
    }
}
