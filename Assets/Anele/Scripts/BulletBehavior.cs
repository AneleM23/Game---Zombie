using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet has collided with an enemy
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle") || other.CompareTag("Ground"))
        {
            // Destroy the bullet if it hits obstacles or ground
            Destroy(gameObject);
        }
    }

    public void OnBecameInvisible()
    {
        // Destroy the bullet when it goes off-screen (camera view point)
        Destroy(gameObject);
    }
}
