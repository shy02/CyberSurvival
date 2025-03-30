using UnityEngine;
using System.Collections;

public class DashAttack : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.5f;
    private Transform player;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Dash()
    {
        if (player == null || isDashing) return;

        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        animator.SetBool("isDash", true);
        Vector2 dashDirection = (player.position - transform.position).normalized;

        rb.linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = Vector2.zero;
        isDashing = false;
        animator.SetBool("isDash", false);
    }
}
