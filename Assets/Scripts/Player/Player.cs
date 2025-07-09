using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float velocidadOriginal;
    private Coroutine drenandoVida;
    private Health health;

    private bool controlesInvertidos = false;
    private Coroutine drainHealthCoroutine;
    public bool tiempoActivo = false;
    [Header("Movimiento")]
    public float speed = 5;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Vector3 direction;
    public Vector2 ultimaDireccionMovimiento = Vector2.right;

    [Header("Cambio de Color")]
    private SpriteRenderer spriteRenderer;

    [Header("Vida")]
    public int damage;
    public int currentHealth;
    public int maxhealth;
    private float healingProgress = 0f;
    public string namePlayer;
    public bool fueGolpeado;
    public bool isKnockedback;

    [Header("Eventos")]
    public GameEvents knockBack;
    public PlayerEvents _playerEvents;
    public AudioSource _audioSteps;
    private PlayerBehabiurs anim;



    // public PlayerAttack pa;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<PlayerBehabiurs>();
        ChangeColor(Color.yellow);
        health = GetComponent<Health>();
        knockBack.Raise(2);

        //  pa.OnAttack += SetAttackAnim;
    }

    private void Update()
    {
        Direction();

    }
    /* public void SetAttackAnim(int value)
     {
         anim.SetTrigger("Attack");
     }*/


    private void FixedUpdate()
    {
        if (!isKnockedback)
        {
            MovePlayer();
        }
    }

    void Direction()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (controlesInvertidos)
        {
            horizontal *= -1;
            vertical *= -1;
        }

        direction = new Vector3(horizontal, vertical, 0);

        if (direction != Vector3.zero)
        {
            ultimaDireccionMovimiento = direction.normalized;
        }
    }

    public void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (controlesInvertidos)
        {
            horizontal *= -1;
            vertical *= -1;
        }

        if ((horizontal > 0 && transform.localScale.x < 0) || (horizontal < 0 && transform.localScale.x > 0))
        {
            Flip();
            ActiveEfectsMovement();
        }
        else
        {
            DesactiveEfectsMovement();
        }
        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;

    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void ActiveEfectsMovement()
    {
        anim.AnimWalkOn();
        _audioSteps.enabled = true;
    }
    void DesactiveEfectsMovement()
    {
        anim.AnimWalkOff();
        _audioSteps.enabled = false;
    }
    void ChangeColor(Color newColor)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }
    }

    public void knockback(Transform enemy, float force, float stunTime)
    {
        if (currentHealth > 0)
        {
            isKnockedback = true;
            Vector2 dir = (transform.position - enemy.position).normalized;
            rb.linearVelocity = dir * force;
            //_playerEvents.Knockback.OnEventRaise(stunTime);
        }
    }

    public void KnockbackCounter(float stunTime)
    {

        if (!gameObject.activeInHierarchy) return;

        StartCoroutine(KnockbackCounterCoroutine(stunTime));
    }

    IEnumerator KnockbackCounterCoroutine(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        isKnockedback = false;
    }

    public void TakeDamage(int damage)
    {
        health.characterHealth -= damage;
        Debug.Log("TakeDamage +" + damage);
        if (health.characterHealth <= 0)
        {
            _playerEvents.Died.OnEventRaise();
        }
    }
    public void HealLife(int heal)
    {
        health.characterHealth += (int)(heal);

        if (health.characterHealth >= health.maxHealth)
        {
            health.characterHealth = health.maxHealth;
        }
    }
    public void HealLife(float timeCast)
    {
        if (health.characterHealth >= health.maxHealth || timeCast <= 0f)
            return;

        float totalHealAmount = health.maxHealth - health.characterHealth;
        float healRatePerSecond = totalHealAmount / timeCast;

        healingProgress += healRatePerSecond * Time.deltaTime;

        if (healingProgress >= 1f)
        {
            int healNow = Mathf.FloorToInt(healingProgress);
            health.characterHealth += healNow;
            healingProgress -= healNow;

            if (health.characterHealth > health.maxHealth)
                health.characterHealth = health.maxHealth;
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void DesactivePlayer()
    {
        this.enabled = false;
        GetComponent<PlayerPowerStates>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        rb.linearVelocity = Vector2.zero;
    }

    public void ActivePlayer()
    {
        this.enabled = true;
        GetComponent<PlayerPowerStates>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Deff_Luz()
    {

    }

    public void Deff_Oscuridad()
    {

    }

    public void Deff_Distorsion()
    {
        if (controlesInvertidos)
        {
            controlesInvertidos = true;
        }
        else
        {
            controlesInvertidos = false;

        }
    }
    public void Deff_Tiempo()
    {
        //Cada que se utiliza detener el tiempo el personaje pierde un poco de vida, mientras se siga manteniendo la habilidad activa, hasta que se presione nuevamente la tecla espacio y se desactive, pero los enemigos se mantienen quietos.

        if (!tiempoActivo)
            ActivarTiempo();
    }


    public void Deff_TiempoOff()
    {


        if (tiempoActivo)
            DesactivarTiempo();
    }

    IEnumerator DrainHealthOverTime()
    {
        while (tiempoActivo)
        {
            TakeDamage(5);
            yield return new WaitForSeconds(1f);
        }
    }

    public void OnStateChanged(Estados nuevoEstado)
    {
        controlesInvertidos = false;
        if (tiempoActivo && nuevoEstado != Estados.Tiempo)
        {
            tiempoActivo = false;

            speed = velocidadOriginal;

            if (drainHealthCoroutine != null)
            {
                StopCoroutine(drainHealthCoroutine);
                drainHealthCoroutine = null;
            }

            Enemy[] enemigos = FindObjectsOfType<Enemy>();
            foreach (Enemy enemigo in enemigos)
                enemigo.RestaurarTiempo();
        }

        switch (nuevoEstado)
        {
            case Estados.Distorsion:
                controlesInvertidos = true;
                break;

            case Estados.Tiempo:
                break;
        }
    }

    public void ActivarTiempo()
    {
        tiempoActivo = true;
        velocidadOriginal = speed;
        speed /= 2f;

        Enemy[] enemigos = FindObjectsOfType<Enemy>();
        foreach (Enemy enemigo in enemigos)
            enemigo.DetenerTiempo();

        if (drainHealthCoroutine == null)
            drainHealthCoroutine = StartCoroutine(DrainHealthOverTime());
    }

    public void DesactivarTiempo()
    {
        tiempoActivo = false;
        speed = velocidadOriginal;

        Enemy[] enemigos = FindObjectsOfType<Enemy>();
        foreach (Enemy enemigo in enemigos)
            enemigo.RestaurarTiempo();

        if (drainHealthCoroutine != null)
        {
            StopCoroutine(drainHealthCoroutine);
            drainHealthCoroutine = null;
        }
    }

}