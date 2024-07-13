using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public int maxHealth = 3;
    public Transform target;
    public enum EnemyType { Easy, Medium, Hard }
    public EnemyType enemyType;
    public GameObject coinPrefab;
    public GameObject healthPrefab;
    public GameObject ammoPrefab;

    private int currentHealth;
    private Rigidbody2D rb;
    public Animator animator;
    public bool isWalking;
    public bool isAttacking;
    public bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            MoveTowardsTarget();
        }
        Animate();
    }

    void MoveTowardsTarget()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    void Animate()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isDead", isDead);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        DropResource();
        Destroy(gameObject, 1f); // Delay destruction to allow the death animation to play
    }

    void DropResource()
    {
        switch (enemyType)
        {
            case EnemyType.Easy:
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
                break;
            case EnemyType.Medium:
                Instantiate(healthPrefab, transform.position, Quaternion.identity);
                break;
            case EnemyType.Hard:
                Instantiate(ammoPrefab, transform.position, Quaternion.identity);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            // Implement attack behavior here
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }
}
