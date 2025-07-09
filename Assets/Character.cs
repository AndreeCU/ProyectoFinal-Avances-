using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    public float moveSpeed = 5f;
    public float maxHealth = 100;
    protected float currentHealth;
    public float CurrentHealth => currentHealth;

    protected Rigidbody2D rb;
    protected Animator animator;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public abstract void HandleInput();

    public virtual void TakeDamage(int amount, Vector2 direction)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator?.Play("Hit"); // Opcional
            rb.AddForce(-direction.normalized * 2f, ForceMode2D.Impulse);
        }
    }

    protected virtual void Die()
    {
        rb.linearVelocity = Vector2.zero;
        animator?.Play("Die");
    }
}