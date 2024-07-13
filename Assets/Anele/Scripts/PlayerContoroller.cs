using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoroller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    private bool isGrounded = false;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        Shoot();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip the character based on the movement direction
        if (moveInput > 0)
            transform.localScale = new Vector3(2, 2, 2);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-2, 2, 2);

        // Update animator parameters
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            float jumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * jumpHeight);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            Debug.Log("Jump!");
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Instantiate the bullet prefab at the firePoint position with no rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Get the Rigidbody2D component from the bullet
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                // Determine the direction based on the player's facing direction
                Vector2 shootDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

                // Set the bullet's velocity in the shoot direction
                bulletRb.velocity = shootDirection * bulletSpeed;

                // Optionally, you can add a small force to the bullet to make sure it moves
                // bulletRb.AddForce(shootDirection * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Grounded");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("Not Grounded");
        }
    }
}

