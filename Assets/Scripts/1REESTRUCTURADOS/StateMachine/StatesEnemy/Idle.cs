using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public GameObject ShadowControl;
    public bool insteractable = false;
    public bool canControll = false;
    public override void LoadComponent()
    {
        base.LoadComponent();
    }

    private void Awake()
    {
        LoadComponent();
    }
    public override void Enter()
    {
        if (!canControll)
        {
            if (ShadowControl != null)
                ShadowControl.SetActive(false);
        }
    }
    void Start()
    {

    }

    public override void Execute()
    {       
        if (canControll)
        {
            m_MachineState.NextState(TypeState.Mover);
        }
        if (healt.isDeath)
        {
            m_MachineState.NextState(TypeState.Muerte);
        }
    }

    void Update()
    {
        Execute();
    }
    public override void Exit()
    {
        canControll = false;
    }


}