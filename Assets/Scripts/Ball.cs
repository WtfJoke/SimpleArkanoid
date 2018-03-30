using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float zSpeed;
    public float xMax;
    private Vector3 velocity;
    private bool started;
    private Paddle paddle;
    // avoid multiple velocity changes when destroying multiple bricks at same time
    private bool brickHitWithVelocityChange;

    // Use this for initialization
    void Start()
    {
        velocity = new Vector3(0, 0, zSpeed);
        started = true;
        paddle = GameObject.FindObjectOfType<Paddle>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        ShootBallWithSpace();
        brickHitWithVelocityChange = false;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                velocity = GetNormalicedVelocity(other);
                gameObject.GetComponent<AudioSource>().Play();
                break;
            case "Brick":
                if (!brickHitWithVelocityChange)
                {
                    velocity = GetNormalicedVelocity(other);
                    brickHitWithVelocityChange = true;
                }
                other.gameObject.GetComponent<Brick>().Hit();
                break;
            case "Wall":
                velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
                break;
            case "HorizontalWall":
                velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
                break;
            case "LostBallWall":
                GameManager.instance.Lifes--;
                Respawn();
                break;
        }
    }

    public void Respawn()
    {
        StickToPaddle();
        started = false;
    }

    private Vector3 GetNormalicedVelocity(Collider other)
    {
        float maxDistance = transform.localScale.x * 0.5f + other.transform.localScale.x * 0.5f;
        float actualDistance = transform.position.x - other.transform.position.x;
        float normalizedDistance = actualDistance / maxDistance;

        return new Vector3(normalizedDistance * xMax, velocity.y, -velocity.z);
    }


    private void ShootBallWithSpace()
    {
        if (started)
        {
            return;
        }
        else
        {
            StickToPaddle();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity = new Vector3(0, 0, -zSpeed);
                transform.parent = null;
                started = true;
            }
        }
    }

    private void StickToPaddle()
    {
        Vector3 playerPosition = paddle.transform.position;
        Vector3 respawnPosition = playerPosition + new Vector3(0, 0, 1f);
        transform.position = respawnPosition;
        transform.parent = paddle.transform;
    }


}
