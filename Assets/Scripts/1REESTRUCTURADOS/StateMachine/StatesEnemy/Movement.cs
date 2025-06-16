using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Movement : State
{

    private Health targetEmemy;
    private Transform target;
    private Rigidbody2D rgb;
    private Vector3 direction;
    public int kindMovement=0;
    private float frameRate = 0;
    private float frameRateForce = 0;
    private bool isCharging = false;
    private void Awake()
    {
        LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
        targetEmemy = enemyIa.healthPlayer;
    }
    void Start()
    {
        Enter();

    }
    public override void Enter()
    {
        if (animato != null)
            animato.SetBool("IsMove", true);

        
    }

    void Update()
    {
        Execute();
    }
    void FixedUpdate()
    {
        Execute();
    }
    public override void Execute()
    {
       
        SelectKindMovement();
        if (healt.isDeath)
            m_MachineState.NextState(TypeState.Muerte);
        if((enemyIa.targetPlayer.position - transform.position).magnitude< enemyIa.distanceToChange)
        {
            m_MachineState.NextState(TypeState.Atacar);
        }
    }
    void CheckingPlayerIsAlive()
    {
        if (enemyIa.healthPlayer != null)
        {
            enemyIa.direction = (enemyIa.targetPlayer.position - transform.position).normalized;
        }
        else
        {
            //if()
            m_MachineState.NextState(TypeState.Idle);
        }
       
    }
    void SelectKindMovement()
    {

        switch (kindMovement)
        {
            case 0:
                MovementTowardPlayer();
                break;
            case 1:
                MovementWanderToPlayer();
                break;
            case 2:
                MovementWaitToPlayer();
                break;
        }
    }
    void MovementTowardPlayer()
    {
        CheckingPlayerIsAlive();
        Vector3 force = enemyIa.direction * enemyIa.speed;
        enemyIa.rb.linearVelocity = (force);
    }
    void MovementWanderToPlayer()
    {
        CheckingPlayerIsAlive();
        float noise = Mathf.PerlinNoise(Time.time * enemyIa.wanderFrequency, 0f) * 2 - 1;
        Vector3 perpendicular = Vector3.Cross(enemyIa.direction, Vector3.forward).normalized;
        Vector3 wanderDirection = (enemyIa.direction + perpendicular * noise * enemyIa.wanderAmplitude).normalized;

        Vector3 force = wanderDirection * enemyIa.speed;
        enemyIa.rb.linearVelocity = force;
    }
    void MovementWaitToPlayer()
    {
        if (enemyIa.healthPlayer != null)
        {
            if (!isCharging)
            {
                frameRate += Time.deltaTime;
                enemyIa.direction = (enemyIa.targetPlayer.position - transform.position).normalized;
                enemyIa.rb.linearVelocity = Vector2.zero;

                if (frameRate >= enemyIa.waitTime)
                {
                    isCharging = true;
                    frameRate = 0f;
                }
            }
            else
            {
                frameRateForce += Time.deltaTime;
                Vector3 force = enemyIa.direction * (enemyIa.speed * 2f); // velocidad de carga
                enemyIa.rb.linearVelocity = force;

                if (frameRateForce >= enemyIa.waitTime / 2)
                {
                    isCharging = false;
                    frameRateForce = 0f;
                    enemyIa.rb.linearVelocity = Vector3.zero;
                }
            }
        }
         
    }
    public override void Exit()
    {
        StopMovement();
        if (animato != null)
            animato.SetBool("IsMove", false);
    }
    void StopMovement()
    {
        enemyIa.rb.linearVelocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            healt.UpdateHealth(2);
            //m_MachineState.NextState(TypeState.Atacar);
        }
    }

    private void OnDrawGizmos()
    {
        if (enemyIa.healthPlayer != null)
        {
            // Dibuja una línea hacia el objetivo
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, enemyIa.targetPlayer.position);
            Gizmos.DrawSphere(enemyIa.targetPlayer.position, 0.3f); // Marca el objetivo con una esfera
        }
  
    }

}