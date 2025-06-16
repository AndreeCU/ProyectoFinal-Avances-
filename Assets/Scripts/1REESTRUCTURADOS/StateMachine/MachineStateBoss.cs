using UnityEngine;

public class MachineStateBoss : MonoBehaviour
{
    public StateBoss CurrentState;
    public StateBoss[] m_States;
    public TypeStateBoss stateDefaul;
    public Animator animato;

    private void Start()
    {

        m_States = GetComponents<StateBoss>();
        foreach (var item in m_States)
        {
            if (item.type == stateDefaul)
            {
                item.Enter();

                item.enabled = true;

                CurrentState = item;

            }
            else
            {
                item.enabled = false;
            }
        }
    }

    public void NextState(TypeStateBoss state)
    {
        foreach (var item in m_States)
        {
            if (item.type == state)
            {
                if (CurrentState != null)
                {
                    CurrentState.Exit();
                    CurrentState.enabled = false;

                    CurrentState = item;
                    CurrentState.enabled = true;
                    CurrentState.Enter();
                }
            }
        }
    }

    public void DesactiveStateAll()
    {
        foreach (var item in m_States)
        {
            item.enabled = false;
        }
    }
}
