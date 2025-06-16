using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Died : State
{
    public float timeToDeath=2f;
    private float FrameRate;
    private bool puntosOtorgados = false;
    [SerializeField] private float cantidadPuntos = 10f;
    private Puntos puntaje;
    public override void LoadComponent()
    {
        base.LoadComponent();
    }

    private void Awake()
    {
        LoadComponent();
        puntaje = FindObjectOfType<Puntos>();
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

        if (!puntosOtorgados && puntaje != null)
        {
            puntaje.Sumarpuntos(cantidadPuntos);
            puntosOtorgados = true;
            Debug.Log("Puntos otorgados por enemigo muerto: " + cantidadPuntos);
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