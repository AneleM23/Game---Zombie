using System.Collections;
using System.Collections.Generic;
using UnityEngine;        
using UnityEngine.Tilemaps;

public class BulletBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
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

    private void OnBecameInvisible()
    {
        // Destroy the bullet when it goes off-screen (camera view point)
        Destroy(gameObject);
    }
}
