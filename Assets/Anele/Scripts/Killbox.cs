using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
   public  PlayerHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerContoroller player = other.GetComponent<PlayerContoroller>();
            if (player != null)
            {
                player.Respawn();
                Debug.Log("Player has respawned.");
                playerHealth.TakeDamage(1);
            }
        }
        else
        {
            Destroy(other.gameObject);
            Debug.Log("An object has entered the killbox and has been destroyed.");
        }
    }



}
