using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public Vector3 speed;
	public Transform floor;

    private Vector3 originalScale;
    private int enlargeConsumed = 0;
    private static int MAXENLARGECONSUMED = 2;

	void Start () {
        originalScale = transform.localScale;
	}
	
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
                if (enlargeConsumed < MAXENLARGECONSUMED)
                {
                    ChangeScaleWithoutBallScale(new Vector3(transform.localScale.x * 2, transform.localScale.y, transform.localScale.z));
                    AudioManager.instance.powerUpCollected.Play();
                    enlargeConsumed++;
                }
                break;
            case PowerUp.PowerUpType.Shrink:
                ChangeScaleWithoutBallScale(new Vector3(transform.localScale.x / 2, transform.localScale.y, transform.localScale.z));
                AudioManager.instance.powerDownCollected.Play();
                if (enlargeConsumed > 0)
                {
                    enlargeConsumed--;
                }
                break;
            case PowerUp.PowerUpType.TwoBalls:
                Ball ball = DetachBall();
                GameManager.instance.SpawnBall();
                ReAttachBall(ball);
                AudioManager.instance.powerUpCollected.Play();
                break;
            default:
                break;
        }
        Destroy(powerUp.gameObject);

    }

    private void ChangeScaleWithoutBallScale(Vector3 newScale)
    {
        // Detach ball
        Ball ball = DetachBall();

        // change scale
        transform.localScale = newScale;

        // Reattach ball
        ReAttachBall(ball);
    }

    private void ReAttachBall(Ball ball)
    {
        if (ball == null)
        {
            return;
        }
        ball.transform.parent = transform;
    }

    private Ball DetachBall()
    {
        Ball ball = gameObject.GetComponentInChildren<Ball>();
        if (ball != null)
        {
            ball.transform.parent = null;
        }
        
        return ball;
    }

    public void ResetScale()
    {
        ChangeScaleWithoutBallScale(originalScale);
        enlargeConsumed = 0;
    }
}
