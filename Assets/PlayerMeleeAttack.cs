using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public float attackRange = 1f;
    public int damage = 25;
    public float attackCooldown = 0.5f;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public Animator animator;

    private float lastAttackTime;
    private bool isAttacking = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private System.Collections.IEnumerator PerformAttack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        if (animator != null)
        {
            animator.Play("Attack");
        }

        // Esperamos un poco antes de aplicar daño (ajusta este tiempo según tu animación)
        yield return new WaitForSeconds(0.2f);

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in enemiesHit)
        {
            IDamageable target = enemy.GetComponent<IDamageable>();
            if (target != null)
            {
                Vector2 direction = enemy.transform.position - transform.position;
                target.TakeDamage(damage, direction);
            }
        }

        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    public void HitFrame()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in enemiesHit)
        {
            IDamageable target = enemy.GetComponent<IDamageable>();
            if (target != null)
            {
                Vector2 direction = enemy.transform.position - transform.position;
                target.TakeDamage(damage, direction);
            }
        }
    }
}