using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public EnemyIa enemyIa;
    private int damage = 10;
    public float knockbackForce= 10;
    public float stunTime= 0.5f;
    private Player player;
    private void Start()
    {
        if (GetComponent<EnemyIa>() != null)
        {
            enemyIa = GetComponent<EnemyIa>();
            damage = enemyIa.damage;
        }       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health health = collision.gameObject.GetComponent<Health>();
            player = collision.gameObject.GetComponent<Player>();
            Debug.Log("OnCollisionEnter2D");
            if (player != null)
            {
                player.knockBack.Raise(damage);
                Debug.Log("player + OnCollisionEnter2D+   player.knockBack.Raise(damage)");

                if (health.characterHealth > 0)
                {
                    player.knockBack.Raise(stunTime);
                    Debug.Log("player + OnCollisionEnter2D+   player.knockBack.Raise(stunTime);");

                    //player._playerEvents.Knockback.OnEventRaise(stunTime);
                    player.knockback(transform, knockbackForce, stunTime);
                }
            }
          
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }
}
