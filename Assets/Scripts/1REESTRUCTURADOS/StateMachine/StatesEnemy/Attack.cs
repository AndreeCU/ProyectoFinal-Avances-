using UnityEngine;

public class Attack : State
{
    public DamagePopup damagePopup;
    public float velocityAttack=0.75f;
    //public float damage;
    public float FrameRate;
    public bool justAttack;


    public override void LoadComponent()
    {
        base.LoadComponent();
    }

    private void Awake()
    {
        LoadComponent();
    }
    void Start()
    {
        Enter();
    }
    public override void Enter()
    {
        base.Enter();

    }
    void PrintDamage()
    {
        Vector3 hitPosition = enemyIa.targetPlayer.position;
        DamagePopup popup = Instantiate(damagePopup, hitPosition, Quaternion.identity);
        popup.Setup(enemyIa.damage);
        popup.DestroyAfter(0.75f);
        
    }

    void Update()
    {
        Execute();
    }
    public override void Execute()
    {
        base.Execute();
        DoDamageToPlayer();
        if ((enemyIa.targetPlayer.position - transform.position).magnitude > enemyIa.distanceToChange)
        {
            m_MachineState.NextState(TypeState.Mover);
        }
    }
    void DoDamageToPlayer()
    {
        if (!justAttack)
        {
            FrameRate += Time.deltaTime;
            if (FrameRate >= velocityAttack)
            {                
                justAttack = true;
            }
        }
        else
        {
            PrintDamage();
            FrameRate = 0;
            justAttack = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().knockBack.Raise(enemyIa.damage);
            PrintDamage();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //m_MachineState.NextState(TypeState.Mover);
        }
    }

}
