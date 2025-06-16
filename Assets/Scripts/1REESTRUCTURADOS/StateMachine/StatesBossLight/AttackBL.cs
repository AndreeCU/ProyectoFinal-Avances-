using UnityEngine;

public class AttackBL : StateBoss
{

    public float velocityAttack =10f;
    public float timeToMove = 15f;
    //public float damage;
    public float FrameRate;
    public bool justAttack;
    public float FrameRateExit;
    private int Id;
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
    private void OnEnable()
    {
        Id = UnityEngine.Random.Range(0, 3);
        ActiveAttack1(false);
    }
    public override void Enter()
    {
        base.Enter();       
        ActiveAttack1(true);    
        enemyIa.rb.linearVelocity = Vector2.zero;
    }
    void Update()
    {
        Execute();
    }
    public override void Execute()
    {
 
        base.Execute();
        DoDamageToPlayer();
        ExitAttack();
    }
    void ExitAttack()
    {
        FrameRateExit += Time.deltaTime;
        if (FrameRateExit >= timeToMove)
        {
            m_MachineState.NextState(TypeStateBoss.Mover);
        }
    }
    void DoDamageToPlayer()
    {
        //if (!justAttack)
        //{
        //    FrameRate += Time.deltaTime;
        //    if (FrameRate >= velocityAttack)
        //    {
        //        justAttack = true;
               
        //    }
        //}
        //else
        //{
            
        //    FrameRate = 0;
        //    justAttack = false;
            
        //}
    }
    void ActiveAttack1(bool _fire)
    {
     
        for (int i = 0;i< bossLight.vitalOrb.Length; i++)
        {
            bossLight.vitalOrb[i]._fire = _fire;
            bossLight.vitalOrb[i].Id = Id;
        }
    }
    public override void Exit()
    {
        base.Exit();
       
       
        FrameRateExit = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }

}
