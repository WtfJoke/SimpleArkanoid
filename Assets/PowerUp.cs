using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public float Speed = 3f;
    public PowerUpType type;
    public enum PowerUpType { Enlarge, Shrink, TwoBalls };

    void Update()
    {
        float newPos = transform.position.z + Time.deltaTime * -Speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, newPos);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LostBallWall"))
        {
            Destroy(gameObject);
        }
    }
}
