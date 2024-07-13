using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int damage;

    //Ui ui;
    //Checkpoint check;

    //private Vector3 respawnPoint;

//<<<<<<< HEAD
    public GameObject lose;
//=======
    public GameObject enemyDeathParticle;

//>>>>>>> 45519b112524308f883212591a820670186a9856
    private void Start()
    {
        //respawnPoint = this.gameObject.transform.position;
        //ui = GameObject.Find("UI Image").GetComponent<Ui>();
       // Debug.Log(respawnPoint);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                //if (ui.Lives > 1)
                //{
                //    ui.Lives--;
                //    this.gameObject.transform.position = respawnPoint;
                //    //ui.ReplaceActiveCharacter();
                //}
                //else
                //{
                    Die();
                //}
                
            }
                

            if (gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(EnemyDeath());
            }
               
        }
        
    }

    public void SetHP(int hp)
    {
        health = hp;
    }

    public void Die()
    {
            lose.SetActive(true); 
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Checkpoints")
    //    {
    //        respawnPoint = transform.position;
    //        collision.GetComponent<Collider2D>().enabled = false;
    //        collision.GetComponent<Animator>().SetTrigger("appear");
    //        collision.GetComponent<Animator>().SetTrigger("Idle");
    //    }
    //}

    IEnumerator EnemyDeath()
    {
        GameObject particle = Instantiate(enemyDeathParticle, gameObject.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        Destroy(particle);
        Destroy(gameObject);
    }
}
