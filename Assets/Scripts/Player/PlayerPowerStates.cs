using UnityEngine;

public enum Estados
{
    Luz,
    Oscuridad,
    Distorsion,
    Tiempo
}
public class PlayerPowerStates : MonoBehaviour
{
    private SoundEmitter currentAudio;
    [Header("Estado inicial")]
    public Estados currentColorState = Estados.Luz;

    [Header("Componentes")]
    private Player player;
    private Health playerHealth;
    private PlayerEvents playerEvents;
    private PlayerStyle playerStyle;
    [Header("Habilidades")]
    [SerializeField] private float curacionTime = 4f;
    public float cooldownTiempo = 5f;
    public float cooldownDistorsion = 2f;

    private bool puedeUsarTiempo = true;
    private bool puedeUsarDistorsion = true;

    [Header("Eventos")]
    public SoundEmitter _dash;
    public SoundEmitter _attack;
    public SoundEmitter _oscuridad;
    public GameObject _oscuridadFace;
    public SoundEmitter _luz;
    public SoundEmitter _espacio;
    public SoundEmitter _tiempo;
    private PlayerBehabiurs anim;

    private void Start()
    {
        anim = GetComponent<PlayerBehabiurs>();
        currentColorState = Estados.Luz;
        player = GetComponent<Player>();
        playerEvents = GetComponent<PlayerEvents>();
        playerStyle = GetComponent<PlayerStyle>();
        playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        SelectStatePower();
        ActivarPoderes();

    }

    void SelectStatePower()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(currentAudio != null) Destroy(currentAudio);
            playerEvents.Debuff_Luz.OnEventRaise();
            playerStyle.ChangeColor(Color.yellow);
            currentColorState = Estados.Luz;
            player.OnStateChanged(currentColorState);
            currentAudio= Instantiate(_luz);
            _oscuridadFace.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentAudio != null) Destroy(currentAudio);
            playerEvents.Debuff_Oscuridad.OnEventRaise();
            playerStyle.ChangeColor(Color.black);
            currentColorState = Estados.Oscuridad;
            player.OnStateChanged(currentColorState);
            currentAudio = Instantiate(_oscuridad);
            _oscuridadFace.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentAudio != null) Destroy(currentAudio);
            playerEvents.Debuff_Distorsion.OnEventRaise();
            playerStyle.ChangeColor(Color.magenta); 
            currentColorState = Estados.Distorsion;
            player.OnStateChanged(currentColorState);
            currentAudio = Instantiate(_espacio);
            _oscuridadFace.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (currentAudio != null) Destroy(currentAudio);
            playerEvents.Debuff_Tiempo.OnEventRaise();
            playerStyle.ChangeColor(Color.green); 
            currentColorState = Estados.Tiempo;
            currentAudio = Instantiate(_tiempo);
            _oscuridadFace.SetActive(false);

            // Cambiar el estado en el jugador
            player.OnStateChanged(currentColorState);

            // Activar o desactivar si ya estaba activo
            if (player.tiempoActivo)
            {
                player.DesactivarTiempo();
            }
        }
    }


    void ActivarPoderes()
    {
        if (player == null) return;

        switch (currentColorState)
        {
            case Estados.Luz:
                if (Input.GetKey(KeyCode.Space)) HabilidadLuz();
                break;

            case Estados.Oscuridad:
                if (Input.GetKeyDown(KeyCode.Space)) HabilidadOscuridad();
               
                break;

            case Estados.Distorsion:
                if (Input.GetKeyDown(KeyCode.Space) && puedeUsarDistorsion) HabilidadDistorsion();
                break;

            case Estados.Tiempo:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (!player.tiempoActivo && puedeUsarTiempo)
                    {
                        player.ActivarTiempo();
                    }
                    else if (player.tiempoActivo)
                    {
                        player.DesactivarTiempo();
                    }
                }
                break;
        }
        
    }

    void HabilidadLuz()
    {
        if (player.rb.linearVelocity == Vector2.zero && !player.isKnockedback && !player.fueGolpeado)
        {
            if (playerHealth.characterHealth < playerHealth.maxHealth)
            {
                Debug.Log("HabilidadLuz++++");
                playerEvents.Heal.responseFloat(curacionTime);
            }
        }
        else
        {
            Debug.Log("No puedes curarte ahora: debes estar quieto, no golpeado, y sin retroceso.");
        }
    }

    void HabilidadOscuridad()
    {
        Debug.Log("Ataque oscuro activado.");
        playerEvents.Attack.OnEventRaise();
        anim.AnimAttack();
        Instantiate(_attack);
    }
    void HabilidadDistorsion()
    {
        Debug.Log("Teletransporte por distorsi�n.");
        Instantiate(_dash);
        anim.AnimDash();
        Vector2 dir = player.ultimaDireccionMovimiento;
        if (dir == Vector2.zero)
        {
            dir = player.facingDirection == 1 ? Vector2.right : Vector2.left;
        }

        player.transform.position += (Vector3)(dir * 2.5f);
        puedeUsarDistorsion = false;
        Invoke(nameof(ReactivarDistorsion), cooldownDistorsion);
    }

    void ReactivarDistorsion()
    {
        puedeUsarDistorsion = true;
    }

    void ReactivarTiempo()
    {
        Enemy[] enemigos = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach (Enemy enemigo in enemigos)
        {
            enemigo.RestaurarTiempo();
        }

        player.tiempoActivo = false;
        player.StopCoroutine("DrenarVida");
        Invoke(nameof(ReactivarTiempo), cooldownTiempo);
    }
}
