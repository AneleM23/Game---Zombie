using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletBehavior : MonoBehaviour
{
    public GameObject coinPrefab; // Reference to the coin prefab

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet has collided with an enemy
        if (other.CompareTag("Enemy"))
        {
            // Instantiate the coin prefab at the enemy's position
            Instantiate(coinPrefab, other.transform.position, Quaternion.identity);

            Destroy(other.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the bullet
        }
        else if (other.CompareTag("Obstacle") || other.CompareTag("Ground"))
        {
            // Destroy the bullet if it hits obstacles or ground
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            // If the bullet collides with the Tilemap
            Tilemap tilemap = other.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                Vector3 hitPosition = transform.position;
                hitPosition.x -= 0.01f * transform.right.x;
                hitPosition.y -= 0.01f * transform.right.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
            Destroy(gameObject);
        }
    }

    public void OnBecameInvisible()
    {
        // Destroy the bullet when it goes off-screen (camera view point)
        Destroy(gameObject);
    }
}