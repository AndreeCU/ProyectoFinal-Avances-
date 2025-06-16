using UnityEngine;

public class DiedBL : StateBoss
{
    public float timeToDeath = 2f;
    private float FrameRate;

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
        if (animato != null)
            animato.SetBool("IsDead", true);
    }
    void Start()
    {


    }

    public override void Execute()
    {
        OffEnemy();
        WaitToDeath();
    }
    void WaitToDeath()
    {
        FrameRate += Time.deltaTime;
        if (FrameRate >= timeToDeath)
        {
            healt.Death(0f);
        }
    }
    void OffEnemy()
    {
        enemyIa._collider2D.enabled = false;
        enemyIa.mySprite.color = Color.black;
        //enemyIa.rb.enabled = false;
    }
    void Update()
    {
        Execute();
    }
    public override void Exit()
    {

    }


}