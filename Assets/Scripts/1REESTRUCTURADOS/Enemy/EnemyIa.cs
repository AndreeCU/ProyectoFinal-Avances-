using UnityEngine;

public class EnemyIa : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D _collider2D;
    public SpriteRenderer mySprite;
    public Vector3 direction;
    public Transform initPos;

    public Transform targetPlayer;
    public Health healthPlayer;
    [Header("DamageData")]
    public int damage;
    public float knockbackForce = 10;
    public float stunTime = 0.5f;
    [Header("MovementData")]
    public float speed ;
    public float acceleration = 5f;
    public float wanderFrequency = 1.5f;
    public float wanderAmplitude = 0.5f;
    public float waitTime =2f;
    public float distanceToChange = 1f;

    public void SetTargetPlayer(Transform targetPlayer)
    {
        this.targetPlayer = targetPlayer;
    }
    public void SetHealthPlayer(Health healthPlayer)
    {
        this.healthPlayer = healthPlayer;
    }
}
