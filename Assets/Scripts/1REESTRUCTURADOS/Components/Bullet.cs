using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{    
    protected EnemyIa enemyIa;
    public  DamagePopup damagePopup;
    private float acceleration =0;
    public int KindBullet=0;
    private void Start()
    {
        enemyIa = GetComponent<EnemyIa>();
    }
    private void FixedUpdate()
    {
        CheckingPlayerIsAlive();
        MovementTowardPlayer();
        LookTarget();
    }
    void MovementTowardPlayer()
    {
        switch (KindBullet) {
            case 0:
                acceleration = Mathf.Clamp(acceleration + 0.1f, 0, enemyIa.acceleration);
                Vector3 force = enemyIa.direction * (enemyIa.speed + acceleration);
                enemyIa.rb.linearVelocity = (force);
                break;
            case 1:
                //Misil perseguidor
                Vector2 desiredVelocity = enemyIa.direction * enemyIa.speed;
                Vector3 velocityDifference = desiredVelocity - enemyIa.rb.linearVelocity;

                acceleration = enemyIa.acceleration; // nueva variable configurable

                // Aplicamos fuerza proporcional al "gap" de velocidad
                enemyIa.rb.AddForce(velocityDifference.normalized * acceleration);
                break;
            default:
                break;
        }
     

   
    }
    void LookTarget()
    {
        if (enemyIa.targetPlayer == null) return;

        Vector2 direction = enemyIa.targetPlayer.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    void CheckingPlayerIsAlive()
    {
        if (enemyIa.healthPlayer != null)
        {
            enemyIa.direction = (enemyIa.targetPlayer.position - transform.position).normalized;
        }
        else
        {
            enemyIa.healthPlayer.Death(0.2f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            Vector3 hitPosition = enemyIa.targetPlayer.position;
            DamagePopup popup = Instantiate(damagePopup, hitPosition, Quaternion.identity);
            popup.Setup(enemyIa.damage);
            popup.DestroyAfter(0.75f);
            //collision.gameObject.GetComponent<Player>().knockBack.Raise(enemyIa.damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        if (enemyIa.healthPlayer != null)
        {
            // Dibuja una línea hacia el objetivo
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, enemyIa.targetPlayer.position);
            Gizmos.DrawSphere(enemyIa.targetPlayer.position, 0.1f); 
        }

    }
}