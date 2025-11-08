using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Hareket")]
    public float moveSpeed = 7f;
    public float jumpForce = 12f;

    [Header("Zemin Kontrolü")]
    public Transform groundCheck;
    public float groundRadius = 0.18f;
    public LayerMask groundLayer; 

    Rigidbody2D rb;
    bool isGrounded;
    float inputX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        inputX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

    
        if (inputX != 0)
        {
            var s = transform.localScale;
            s.x = Mathf.Abs(s.x) * (inputX > 0 ? 1 : -1);
            transform.localScale = s;
        }

    
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer) != null;

    
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}

