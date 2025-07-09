using UnityEngine;
using TMPro;
public interface IDamageable
{
    void TakeDamage(int amount, Vector2 direction);
}
public class PlayerNew : Character
{
    public TMP_Text puntosTexto;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    public Transform attackPoint;
    private float attackPointOffsetX;
    public int puntos = 0;

    public float cooldownDistorsion = 2f;
    public GameObject efectoDistorsionPrefab;
    private bool puedeUsarDistorsion = true;
    private int facingDirection = 1;
    public Vector2 ultimaDireccionMovimiento;
    private static PlayerNew instancia;
    public void AgregarPuntos(int cantidad)
    {
        puntos += cantidad;
        PlayerPrefs.SetInt("Puntos", puntos);

        if (puntosTexto != null)
            puntosTexto.text = "Puntos: " + puntos;
    }

    protected override void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);

        base.Awake();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        attackPointOffsetX = attackPoint.localPosition.x;

    }

    void Update()
    {
        HandleInput();

        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
            attackPoint.localPosition = new Vector3(Mathf.Abs(attackPointOffsetX), attackPoint.localPosition.y, attackPoint.localPosition.z);
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
            attackPoint.localPosition = new Vector3(-Mathf.Abs(attackPointOffsetX), attackPoint.localPosition.y, attackPoint.localPosition.z);
        }

        ultimaDireccionMovimiento = movement;

        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
            facingDirection = 1;
            attackPoint.localPosition = new Vector3(Mathf.Abs(attackPointOffsetX), attackPoint.localPosition.y, attackPoint.localPosition.z);
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
            facingDirection = -1;
            attackPoint.localPosition = new Vector3(-Mathf.Abs(attackPointOffsetX), attackPoint.localPosition.y, attackPoint.localPosition.z);
        }

        if (Input.GetKeyDown(KeyCode.E) && puedeUsarDistorsion)
        {
            HabilidadDistorsion();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    public override void HandleInput()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        else if (Input.GetKey(KeyCode.D)) moveX = 1f;

        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        else if (Input.GetKey(KeyCode.S)) moveY = -1f;

        movement = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    public override void TakeDamage(int amount, Vector2 direction)
    {
        base.TakeDamage(amount, direction);
        Debug.Log("Jugador recibi� da�o: " + amount);
        // Aqu� puedes agregar UI, sonido, c�mara, etc.
    }

    protected override void Die()
    {
        rb.linearVelocity = Vector2.zero;
        animator?.Play("Die");

        rb.simulated = false;
        this.enabled = false;

        Invoke("ReaparecerEnCheckpoint", 1f);
    }
    private void ReaparecerEnCheckpoint()
    {
        try
        {
            float x = PlayerPrefs.GetFloat("CheckpointX", transform.position.x);
            float y = PlayerPrefs.GetFloat("CheckpointY", transform.position.y);
            float z = PlayerPrefs.GetFloat("CheckpointZ", transform.position.z);

            transform.position = new Vector3(x, y, z);

            currentHealth = maxHealth;
            puntos = PlayerPrefs.GetInt("Puntos", 0);

            rb.simulated = true;
            this.enabled = true;

            animator.Rebind();
            animator.Play("Idle", 0);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al cargar datos del checkpoint: " + e.Message);
        }
    }

    private void HabilidadDistorsion()
    {
        if (!puedeUsarDistorsion) return;

        Debug.Log("Teletransporte por distorsión");

        if (efectoDistorsionPrefab != null)
            Instantiate(efectoDistorsionPrefab, transform.position, Quaternion.identity);

        animator.Play("SaltoTiempo"); // 👈 Aquí directamente sin triggers

        Vector2 dir = ultimaDireccionMovimiento;
        if (dir == Vector2.zero)
            dir = facingDirection == 1 ? Vector2.right : Vector2.left;

        transform.position += (Vector3)(dir.normalized * 2.5f);

        puedeUsarDistorsion = false;
        Invoke(nameof(ReactivarDistorsion), cooldownDistorsion);
    }

    private void ReactivarDistorsion()
    {
        puedeUsarDistorsion = true;
    }

    public void RestaurarVidaCompleta()
    {
        currentHealth = maxHealth;
    }
}