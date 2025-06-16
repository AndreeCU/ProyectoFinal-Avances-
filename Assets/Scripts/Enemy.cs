using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool tiempoDetenido = false;
    private Rigidbody2D rb;
    public Transform player;
    private Vector2 direction;
    private Transform initPos;

    [Header("Vida")]
    private int maxHealth = 10;
    public int currentHealth;
    public string nameEnemy;

    [Header("Movimiento")]
    public float speed;
    private float velocidadOriginal;

    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Puntos puntaje;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidadOriginal = speed;
        currentHealth = maxHealth;

    }
    private void OnEnable()
    {
        if (initPos != null)
            transform.position = initPos.position;
    }
    void Update()
    {
        direction = (player.position - transform.position).normalized;  
    }
    private void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        if (!tiempoDetenido)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Debug.Log("Daño recibido: " + damage + " | Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        if (puntaje != null)
        {
            puntaje.Sumarpuntos(cantidadPuntos);
            Debug.Log(nameEnemy + " derrotado. Se otorgaron " + cantidadPuntos + " puntos.");
        }
        else
        {
            Debug.LogWarning("No se asignó el sistema de puntaje en " + nameEnemy);
        }

        gameObject.SetActive(false);
    }
    public void SetInitPos(Transform init)
    {
        initPos = init;
    }
    public void DetenerTiempo()
    {
        tiempoDetenido = true;
        rb.linearVelocity = Vector2.zero;
        Debug.Log(nameEnemy + ": Tiempo detenido activado.");
    }

    public void RestaurarTiempo()
    {
        tiempoDetenido = false;
        Debug.Log(nameEnemy + ": Tiempo detenido desactivado.");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Player player = collision.gameObject.GetComponent<Player>();
            currentHealth = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            //Player player = collision.gameObject.GetComponent<Player>();
            TakeDamage(200);
        }
    }
}
