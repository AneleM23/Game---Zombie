using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float respawnDelay = 5f;

    public void RespawnEnemy(Vector3 position)
    {
        StartCoroutine(RespawnCoroutine(position));
    }

    private IEnumerator RespawnCoroutine(Vector3 position)
    {
        yield return new WaitForSeconds(respawnDelay);
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }

}
