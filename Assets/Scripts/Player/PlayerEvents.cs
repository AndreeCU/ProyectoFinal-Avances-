using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerEvents : MonoBehaviour
{
    [Header("Components")]
    public Player _player;
    public Health _health;
    public PlayerPowerStates _playerPowerStates;
    public PlayerAttack _playerAttack;
    public SoundsController sounds;
    [Header("Events")]  
    public GameEventListeners Died;
    public GameEventListeners Spawm;
    public GameEventListeners Movement;
    public GameEventListeners Attack;
    public GameEventListeners Knockback;
    public GameEventListeners Heal;
    public GameEventListeners Dash;
    public GameEventListeners TimeStop;
    public GameEventListeners Debuff_Luz;
    public GameEventListeners Debuff_Oscuridad;
    public GameEventListeners Debuff_Distorsion;
    public GameEventListeners Debuff_DistorsionOff;
    public GameEventListeners Debuff_Tiempo;
    public GameEventListeners Debuff_TiempoOff;
    
    void Start()
    {
        _player = GetComponent<Player>();
        _playerPowerStates = GetComponent<PlayerPowerStates>();


        //Died.response+= asd1qw;
    }
    private void OnEnable()
    {
        SuscribeEvents();

    }
    void SuscribeEvents()
    {      
        ////////-----------------Die-----------------------///////
        Died.response += _player.Die;
        ////////-----------------Heal-----------------------///////
        Heal.responseFloat += _player.HealLife;
        
        ////////-----------------Attack-----------------------///////
        Attack.response += _playerAttack.Activettack;
        //Attack.response += PlaySoundAttack;
        ////////-----------------Movement-----------------------///////
        Movement.response += _player.MovePlayer;
        ////////-----------------Knockback-----------------------///////
        Knockback.responseInt += _player.TakeDamage;
        Knockback.responseFloat += _player.KnockbackCounter;
        ////////-----------------Debuffs-----------------------///////
        Debuff_Luz.response += _player.Deff_Luz;
        Debuff_Oscuridad.response += _player.Deff_Oscuridad;
        Debuff_Distorsion.response += _player.Deff_Distorsion;
        Debuff_Tiempo.response += _player.Deff_Tiempo;
    }

    void PlaySoundAttack()
    {
        //sounds.main.PlayOneShot(sounds.SfxSounds[1]);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        DesuscribeEvents();
    }
    void DesuscribeEvents()
    {
        ////////-----------------Attack-----------------------///////
        Attack.response -= _playerAttack.Activettack;
        Died.response -= _player.Die;        
        Movement.response -= _player.MovePlayer;
        Knockback.responseInt -= _player.TakeDamage;
        Knockback.responseFloat -= _player.KnockbackCounter;
        Debuff_Luz.response -= _player.Deff_Luz;
        Debuff_Oscuridad.response -= _player.Deff_Oscuridad;
        Debuff_Distorsion.response -= _player.Deff_Distorsion;
        Debuff_Tiempo.response -= _player.Deff_Tiempo;
    }

}
