using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float zSpeed;
	public float xMax;
    public Vector3 respawnPosition = new Vector3(0,0.5f,0);
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
        switch (other.tag)
        {
            case "Player":
                velocity = GetNormalicedVelocity(other);
                gameObject.GetComponent<AudioSource>().Play();
                break;
            case "Brick":
                velocity = GetNormalicedVelocity(other);
                other.gameObject.GetComponent<Brick>().Hit();
                Destroy(other.gameObject);
                break;
            case "Wall":
                velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
                break;
            case "HorizontalWall":
                velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
                break;
            case "LostBallWall":
                // TODO Game over on lifes <= 0
                GameObject.FindObjectOfType<GameManager>().Lifes--;
                GameObject.FindGameObjectWithTag("Ball").transform.position = respawnPosition;
                break;
        }
	}

    private Vector3 GetNormalicedVelocity(Collider other)
    {
        float maxDistance = transform.localScale.x * 0.5f + other.transform.localScale.x * 0.5f;
        float actualDistance = transform.position.x - other.transform.position.x;
        float normalizedDistance = actualDistance / maxDistance;

        return new Vector3(normalizedDistance * xMax, velocity.y, -velocity.z);
    }
}
