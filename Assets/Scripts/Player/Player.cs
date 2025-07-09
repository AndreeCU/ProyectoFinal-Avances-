using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public class Player : MonoBehaviour
{
    #region Movimiento
    public float speed = 5f;
    public int facingDirection = 1;
    public Vector3 direction;
    public Vector2 ultimaDireccionMovimiento = Vector2.right;

    private Rigidbody2D rb;
    private bool controlesInvertidos = false;
    private bool isKnockedback = false;
    #endregion

    #region Vida y daño
    private Health health;
    private float healingProgress = 0f;
    private Coroutine drainHealthCoroutine;
    public bool tiempoActivo = false;
    private float velocidadOriginal;

    #endregion

    #region Render y audio
    private SpriteRenderer spriteRenderer;
    public AudioSource audioSteps;
    private PlayerBehabiurs anim;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<PlayerBehabiurs>();
        health = GetComponent<Health>();
    }

    private void Start()
    {
        ChangeColor(Color.yellow);
    }

    private void Update()
    {
        LeerEntradaMovimiento();
    }

    private void FixedUpdate()
    {
        if (!isKnockedback)
            MoverJugador();
    }

    #region Entrada y movimiento
    void LeerEntradaMovimiento()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (controlesInvertidos)
        {
            h *= -1;
            v *= -1;
        }

        direction = new Vector3(h, v, 0f);

        if (direction != Vector3.zero)
            ultimaDireccionMovimiento = direction.normalized;
    }

    void MoverJugador()
    {
        if (direction == Vector3.zero)
        {
            DesactivarEfectosMovimiento();
            rb.linearVelocity = Vector2.zero;
            return;
        }

        ActivarEfectosMovimiento();

        if ((direction.x > 0 && transform.localScale.x < 0) ||
            (direction.x < 0 && transform.localScale.x > 0))
            VoltearSprite();

        rb.linearVelocity = direction.normalized * speed;
    }

    void VoltearSprite()
    {
        facingDirection *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void ActivarEfectosMovimiento()
    {
        if (anim != null) anim.AnimWalkOn();
        if (audioSteps != null) audioSteps.enabled = true;
    }

    void DesactivarEfectosMovimiento()
    {
        if (anim != null) anim.AnimWalkOff();
        if (audioSteps != null) audioSteps.enabled = false;
    }
    #endregion

    #region Color helper
    void ChangeColor(Color newColor)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = newColor;
    }
    #endregion

    #region Da�o, curaci�n y muerte
    public void TakeDamage(int damage)
    {
        health.characterHealth -= damage;
        if (health.characterHealth <= 0)
            Morir();
    }

    public void HealInstant(int heal)
    {
        health.characterHealth = Mathf.Min(health.characterHealth + heal, health.maxHealth);
    }

    public void HealOverTime(float castTime)
    {
        if (health.characterHealth >= health.maxHealth || castTime <= 0f) return;

        float totalHeal = health.maxHealth - health.characterHealth;
        float healRate = totalHeal / castTime;
        healingProgress += healRate * Time.deltaTime;

        if (healingProgress >= 1f)
        {
            int healNow = Mathf.FloorToInt(healingProgress);
            health.characterHealth += healNow;
            healingProgress -= healNow;
            if (health.characterHealth > health.maxHealth)
                health.characterHealth = health.maxHealth;
        }
    }

    void Morir()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region Knockback
    public void Knockback(Transform enemy, float force, float stunTime)
    {
        if (health.characterHealth <= 0) return;

        isKnockedback = true;
        Vector2 dir = (transform.position - enemy.position).normalized;
        rb.linearVelocity = dir * force;
        StartCoroutine(KnockbackCounter(stunTime));
    }

    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        isKnockedback = false;
    }
    #endregion

    #region Distorsi�n y Tiempo (coordina con PlayerPowerStates)
    public void OnStateChanged(Estados nuevoEstado)
    {
        controlesInvertidos = (nuevoEstado == Estados.Distorsion);

        if (tiempoActivo && nuevoEstado != Estados.Tiempo)
            DesactivarTiempo();
    }

    public void ActivarTiempo()
    {
        if (tiempoActivo) return;

        tiempoActivo = true;
        velocidadOriginal = speed;
        speed /= 2f;

        foreach (Enemy e in FindObjectsOfType<Enemy>())
            e.DetenerTiempo();

        if (drainHealthCoroutine == null)
            drainHealthCoroutine = StartCoroutine(DrenarVidaContinuamente());
    }

    public void DesactivarTiempo()
    {
        if (!tiempoActivo) return;

        tiempoActivo = false;
        speed = velocidadOriginal;

        foreach (Enemy e in FindObjectsOfType<Enemy>())
            e.RestaurarTiempo();

        if (drainHealthCoroutine != null)
        {
            StopCoroutine(drainHealthCoroutine);
            drainHealthCoroutine = null;
        }
    }

    IEnumerator DrenarVidaContinuamente()
    {
        while (tiempoActivo)
        {
            TakeDamage(5);
            yield return new WaitForSeconds(1f);
        }
    }
    #endregion
}

