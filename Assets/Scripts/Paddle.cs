using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public Vector3 speed;
	public Transform floor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float direction = Input.GetAxis("Horizontal");
		float newPos = transform.position.x + direction * Time.deltaTime * speed.x;
		float paddleScale = transform.localScale.x;
		float floorScale = floor.localScale.x;
		float maxPos = floorScale * 10 * 0.5f - paddleScale * 1 * 0.5f;

		float newPositionWithRangeCheck = Mathf.Clamp (newPos, -maxPos, maxPos);
		transform.position = new Vector3(newPositionWithRangeCheck, transform.position.y, transform.position.z);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PowerUp"))
        {
            return;
        }

        PowerUp powerUp = other.GetComponent<PowerUp>();

        switch (powerUp.type)
        {
            case PowerUp.PowerUpType.Enlarge:
                transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y, transform.localScale.z);
                AudioManager.instance.powerUpCollected.Play();
                break;
            case PowerUp.PowerUpType.Shrink:
                transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y, transform.localScale.z);
                AudioManager.instance.powerDownCollected.Play();
                break;
            case PowerUp.PowerUpType.TwoBalls:
                GameManager.instance.SpawnBall();
                AudioManager.instance.powerUpCollected.Play();
                break;
            default:
                break;
        }
        Destroy(powerUp.gameObject);

    }
}
