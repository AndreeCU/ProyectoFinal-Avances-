using UnityEngine;
public enum TypeStateBoss { Mover, Atacar, SpecialMovement, Muerte, Idle }
public class StateBoss : MonoBehaviour
{
    public TypeStateBoss type;
    public MachineStateBoss m_MachineState;
    protected Health healt;
    protected EnemyIa enemyIa;
    protected Animator animato;
    protected BossLight bossLight;
    protected BossEvents bossEvents;
    public virtual void LoadComponent()
    {
        enemyIa = GetComponent<EnemyIa>();
        m_MachineState = GetComponent<MachineStateBoss>();
        healt = GetComponent<Health>();
        animato = m_MachineState.animato;
        bossLight = GetComponent<BossLight>();
        bossEvents = GetComponent<BossEvents>();
    }
    public virtual void Enter()
    {

    }
    public virtual void Execute()
    {

    }
    public virtual void Exit()
    {

    }
}
