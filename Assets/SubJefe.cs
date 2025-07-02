using System.Collections;
using UnityEngine;

public class SubJefe : MonoBehaviour
{
    public float velocidad = 5f;
    public int vida = 250;
    public Animator animator;
    public Rigidbody2D rb;
    public float retrocesoForce = 3f;
    public float tiempoEntreAtaques = 2f;
    public float tiempoPostAtaque = 1f;

    private bool estaMuerto = false;
    private bool puedeAtacar = true;
    private bool enReposo = false;
    private Transform objetivo;
    private Player jugador;

    void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
        {
            objetivo = playerGO.transform;
            jugador = playerGO.GetComponent<Player>();
        }
    }

    void Update()
    {
        if (estaMuerto || objetivo == null || jugador == null || !puedeAtacar || enReposo) return;

        float distancia = Vector2.Distance(transform.position, objetivo.position);
        Vector2 direccion = (objetivo.position - transform.position).normalized;

        if (distancia < 2f)
        {
            int ataqueElegido = Random.Range(0, 2);
            if (ataqueElegido == 0)
                Ataque1(direccion);
            else
                Ataque2(direccion);
        }
        else
        {
            rb.linearVelocity = direccion * velocidad;
            animator.Play("CorrerCorregido");
        }
    }

    void Ataque1(Vector2 direccion)
    {
        puedeAtacar = false;
        rb.linearVelocity = Vector2.zero;
        animator.Play("AtaqueCorregido");
        AplicarDanioAlJugador(20, direccion);
        StartCoroutine(PostAtaque(direccion));
    }

    void Ataque2(Vector2 direccion)
    {
        puedeAtacar = false;
        rb.linearVelocity = Vector2.zero;
        animator.Play("Ataque2Corregido");
        AplicarDanioAlJugador(40, direccion);
        StartCoroutine(PostAtaque(direccion));
    }

    IEnumerator PostAtaque(Vector2 direccion)
    {
        // Se queda quieto
        yield return new WaitForSeconds(0.4f);
        animator.Play("QuietoCorregido");

        // Retrocede
        rb.AddForce(-direccion.normalized * retrocesoForce, ForceMode2D.Impulse);
        enReposo = true;

        yield return new WaitForSeconds(tiempoPostAtaque);
        rb.linearVelocity = Vector2.zero;
        animator.Play("QuietoCorregido");

        enReposo = false;
        puedeAtacar = true;
    }

    void AplicarDanioAlJugador(int damage, Vector2 direccion)
    {
        if (jugador != null)
        {
            jugador.TakeDamage(damage);
            jugador.knockback(transform, retrocesoForce, 0.5f);

            if (jugador.gameObject.activeInHierarchy)
                jugador.KnockbackCounter(0.5f);
        }
    }

    public void RecibirDanio(int cantidad, Vector2 direccion)
    {
        if (estaMuerto) return;

        vida -= cantidad;
        animator.Play("SiendoGolpeadoCorregido");
        rb.AddForce(-direccion.normalized * retrocesoForce, ForceMode2D.Impulse);

        if (vida <= 0)
        {
            Muerte();
        }
    }

    void Muerte()
    {
        estaMuerto = true;
        rb.linearVelocity = Vector2.zero;
        animator.Play("MuertoCorregido");
        Invoke("Desaparecer", 2f);
    }

    void Desaparecer()
    {
        Destroy(gameObject);
    }
}
