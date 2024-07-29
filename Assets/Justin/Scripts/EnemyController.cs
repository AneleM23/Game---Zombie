using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public int maxHealth = 3;
    public Transform target;
    public float detectionRange = 5f;
    public float attackCooldown = 1.5f;
    public int damage = 1;
    public GameObject stonePrefab;
    public float stoneSpeed = 10f;
    public Transform stoneSpawnPoint;
    public enum EnemyType { Easy, Medium, Hard }
    public EnemyType enemyType;
    public GameObject pointPrefab;
    public GameObject healthPrefab;
    public GameObject ammoPrefab;

    private int currentHealth;
    private Rigidbody2D rb;
    public Animator animator;
    [SerializeField] private bool isAttacking = false;
    private float attackTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null && !isAttacking)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget <= detectionRange)
            {
                Attack();
            }
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                isAttacking = false;
                attackTimer = 0f;
            }
        }

        Animate();
    }

    void Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        ThrowStone();
    }

    void ThrowStone()
    {
        if (stonePrefab != null && stoneSpawnPoint != null)
        {
            GameObject stone = Instantiate(stonePrefab, stoneSpawnPoint.position, stoneSpawnPoint.rotation);
            Rigidbody2D stoneRb = stone.GetComponent<Rigidbody2D>();
            Vector2 direction = (target.position - stoneSpawnPoint.position).normalized;
            stoneRb.velocity = direction * stoneSpeed;
        }
    }

    void Animate()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", isAttacking);
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
        isAttacking = false;
        animator.SetBool("isDead", true);
        DropResource();
        Destroy(gameObject, 1f); // Delay destruction to allow the death animation to play
    }

    void DropResource()
    {
        switch (enemyType)
        {
            case EnemyType.Easy:
                Instantiate(pointPrefab, transform.position, Quaternion.identity);
                break;
            case EnemyType.Medium:
                Instantiate(healthPrefab, transform.position, Quaternion.identity);
                break;
            case EnemyType.Hard:
                Instantiate(ammoPrefab, transform.position, Quaternion.identity);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
            isAttacking = false;
        }
    }

}
