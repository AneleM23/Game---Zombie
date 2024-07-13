using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerContoroller player = other.GetComponent<PlayerContoroller>();
            if (player != null)
            {
                player.SetCheckpoint(transform.position);
                Debug.Log("Checkpoint reached at: " + transform.position);
            }
        }
    }

}
