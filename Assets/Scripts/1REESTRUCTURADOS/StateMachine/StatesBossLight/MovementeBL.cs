using UnityEngine;
using System.Collections;

public class MovementeBL : StateBoss
{
    private Health targetEmemy;
    private Transform target;
    private Rigidbody2D rgb;
    private Vector3 direction;
    public int kindMovement = 0;
    private float frameRate = 0;
    private float frameRateForce = 0;
    private bool isCharging = false;
    private Vector2 targetPosition;
    public float waitTime = 2f;
    public float timeToMove = 15f;
    public float minDistance = 1f;
    public bool isMoving = false;
    [SerializeField] private Transform areaTransform;
    [Header("Rotacion")]
    public float velocidadAngular = 90f;
    public float offset = 1.0f;
    public float radioInicial = 4.5f;
    private float angulo;
    private float radio;
    private bool _attack1 = false;
    private Transform centro;
    private float framerate = 0;
    private float FrameRateExit;
    private void Awake()
    {
        LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
        targetEmemy = enemyIa.healthPlayer;
    }


    void Start()
    {
        Enter();

    }
    public override void Enter()
    {
        if (animato != null)
            animato.SetBool("IsMove", true);
        StartCoroutine(WanderRoutine());
    }
    IEnumerator WanderRoutine()
    {
        if (!isMoving)
        {
            targetPosition = GetRandomPositionFromAreaTransform();
            isMoving = true;
        }
        //Debug.Log(targetPosition);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(WanderRoutine());
    }
    Vector2 GetRandomPositionFromAreaTransform()
    {
        Vector2 center = areaTransform.position;
        Vector2 size = areaTransform.localScale; // usa la escala como tamaño del área

        float x = UnityEngine.Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float y = UnityEngine.Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);

        return new Vector2(x, y);
    }
    void Update()
    {
        Execute();
    }
    void FixedUpdate()
    {
        // Execute();
    }
    public override void Execute()
    {
        ExitMovement();
        SelectKindMovement();
        if (healt.isDeath)
            m_MachineState.NextState(TypeStateBoss.Muerte);

    }
    void SelectKindMovement()
    {
        //MovementCircular();
        switch (kindMovement)
        {
            case 0:
                MovementWander();
                break;
        }
    }
    void ExitMovement()
    {
        FrameRateExit += Time.deltaTime;
        if (FrameRateExit >= timeToMove)
        {
            m_MachineState.NextState(TypeStateBoss.Atacar);
        }
    }
    void MovementWander()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyIa.speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPosition) <= minDistance)
            {
                isMoving = false;
                m_MachineState.NextState(TypeStateBoss.Atacar);
            }
            if ((enemyIa.targetPlayer.position - transform.position).magnitude < enemyIa.distanceToChange)
            {
                targetPosition = GetRandomPositionFromAreaTransform();
            }
        }
    }
    void MovementCircular()
    {
        Vector3 centro = transform.position;

        float rad = angulo * Mathf.Deg2Rad;
        Quaternion rotacion = Quaternion.Euler(0f, 0f, angulo);
        Vector3[] corner = { rotacion * new Vector3(+offset, +offset, 0f), // sup. der
         rotacion * new Vector3(-offset, +offset, 0f), // sup. izq
         rotacion * new Vector3(-offset, -offset, 0f), // inf. izq
         rotacion * new Vector3(+offset, -offset, 0f) }; // inf. der}

        for (int i = 0; i < bossLight.vitalOrb.Length; i++)
        {
            bossLight.vitalOrb[i].transform.position = centro + corner[i];
        }
    }
    public override void Exit()
    {
        FrameRateExit = 0;
        StopMovement();
        if (animato != null)
            animato.SetBool("IsMove", false);

    }
    void StopMovement()
    {
        enemyIa.rb.linearVelocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            healt.UpdateHealth(2);
            //m_MachineState.NextState(TypeState.Atacar);
        }
    }

    private void OnDrawGizmos()
    {
        if (enemyIa != null && enemyIa.healthPlayer != null)
        {
            // Dibuja una línea hacia el objetivo
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, targetPosition);

            if (enemyIa.targetPlayer != null)
            {
                Gizmos.DrawSphere(enemyIa.targetPlayer.position, 0.3f); // Marca el objetivo con una esfera
            }
        }
    }
}