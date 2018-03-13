using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public Vector3 speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float direction = Input.GetAxis("Horizontal");
        transform.position = transform.position + direction * speed * Time.deltaTime;
	}
}
