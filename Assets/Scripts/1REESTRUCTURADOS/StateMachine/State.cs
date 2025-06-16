using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum TypeState { Mover, Atacar, SpecialMovement, Muerte, Idle }
public class State : MonoBehaviour
{
    public TypeState type;
    public MachineState m_MachineState;
    protected Health healt;
    protected EnemyIa enemyIa;
    protected Animator animato;

    public virtual void LoadComponent()
    {
        enemyIa = GetComponent<EnemyIa>();
        m_MachineState = GetComponent<MachineState>();
        healt = GetComponent<Health>();
        if (m_MachineState.animato != null) ;
        animato = m_MachineState.animato;
     
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