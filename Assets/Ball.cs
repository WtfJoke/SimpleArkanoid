using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float zSpeed;
	public float xMax;
	private Vector3 velocity;

	// Use this for initialization
	void Start () {
		velocity = new Vector3 (0, 0, zSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += velocity * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Paddle") {
			
			float maxDistance = transform.localScale.x * 0.5f + other.transform.localScale.x * 0.5f;
			float actualDistance = transform.position.x - other.transform.position.x;
			float normalizedDistance = actualDistance / maxDistance;

			velocity = new Vector3 (normalizedDistance * xMax, velocity.y, -velocity.z);
		}
		else if (other.tag == "Wall") {
			velocity = new Vector3 (-velocity.x, velocity.y, velocity.z);
		}
		gameObject.GetComponent<AudioSource> ().Play ();
	}
}
