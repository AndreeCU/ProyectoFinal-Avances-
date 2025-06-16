using UnityEngine;

public class BossEvents : MonoBehaviour
{
    [Header("System")]
    public Enemy enemy;
    public BossLight bossHealth;
    public MovementeBL bossMovement;
    public AttackBL bossAttack;
    public DiedBL bossDied;
    [Header("Events")]
    public GameEventListeners UpdateBossLife;
    public GameEventListeners Movement;
    public GameEventListeners Appear;
    public GameEventListeners Disappear;
    public GameEventListeners Blink;
    public GameEventListeners Attack1;
    public GameEventListeners Raige;
    public GameEventListeners Tired;
    public GameEventListeners Boom;
    public GameEventListeners Flash;
    private void Start()
    {
        //Attack1.response += ActiveAttack1;
        //Disappear.response += Hide;
    }
    private void OnEnable()
    {
        UpdateBossLife.responseFloat += bossHealth.UpdateHealth;

    }
    private void OnDisable()
    {
        UpdateBossLife.responseFloat -= bossHealth.UpdateHealth;

    }
}
