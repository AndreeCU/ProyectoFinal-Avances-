using UnityEngine;

public class EnemyNew : Character
{
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    public int attackDamage = 10;

    private Transform target;
    private float lastAttackTime;

  

    protected override void Awake()
    {
        base.Awake();
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            target = playerObj.transform;
    }

    void Update()
    {
        if (target == null || currentHealth <= 0) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= detectionRange)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            if (distance > attackRange)
            {
                // Caminar hacia el jugador
                rb.linearVelocity = direction * moveSpeed;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    Attack(direction);
                    lastAttackTime = Time.time;
                }
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Attack(Vector2 direction)
    {
        IDamageable targetDamageable = target.GetComponent<IDamageable>();
        if (targetDamageable != null)
        {
            targetDamageable.TakeDamage(attackDamage, direction);
            Debug.Log("�El enemigo atac� al jugador!");
        }
    }

    protected override void Die()
    {
        rb.linearVelocity = Vector2.zero;
        animator?.Play("EnemyDie");

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            PlayerNew player = playerObj.GetComponent<PlayerNew>();
            if (player != null)
            {
                player.AgregarPuntos(50);
            }
        }

        Destroy(gameObject, 1f);
    }

    public override void HandleInput()
    {
        // No lo usa; requerido por herencia
    }

    public override void TakeDamage(int amount, Vector2 direction)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;

        rb.AddForce(-direction.normalized * 5f, ForceMode2D.Impulse); // Mayor retroceso
        animator?.Play("RecibiendoAtaque");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
