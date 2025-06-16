using System;
using System.Collections;
using UnityEngine;


public class Bullets : MonoBehaviour
{
    private Transform initPos;
    public int damagebullet;
    public bool Kind;
    [Header("Movimiento")]
    public Transform target;
    public float velocityArrow;
    private float _velocityArrow;
    public Vector3 direction;
    private Rigidbody2D rigidbody2D;
    [Header("Rotacion")]
    public Transform centro;
    public float velocidadAngular = 90f;  
    public float velocidadExpansion = 1f; 
    public float radioInicial = 1f;
    private float angulo; 
    private float radio;
    [Header("Events")]
    public GameEventListeners _attackPlayer;

    public float framerate=0;
    private float timeToExpand = 4;

    public void SetInitPos(Transform init)
    {
        initPos = init;
      
    }
    public void SetDamage(int damageArrow)
    {
        this.damagebullet = damageArrow;
    }
    public void SetCentro(Transform centro)
    {
        this.centro = centro;
    }
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        radio = radioInicial;
        _velocityArrow = velocityArrow;
    }
    private void Update()
    {
        if(Kind)
            MovementCircularSpheres();

        framerate += Time.deltaTime;
        
    }

    void MovementCircularSpheres()
    {
        if (centro != null)
        {
            angulo += velocidadAngular * Time.deltaTime;
            radio += velocidadExpansion * Time.deltaTime;

            float rad = angulo * Mathf.Deg2Rad;

            float x = Mathf.Cos(rad) * radio;
            float y = Mathf.Sin(rad) * radio;

            transform.position = centro.position + new Vector3(x, y, 0f);

            transform.rotation = Quaternion.identity;
        }
    }
    private void FixedUpdate()
    {
        if (!Kind)
        {
            if (framerate <= timeToExpand)
            {
                Debug.Log("FireExpand()");
                FireExpand();
            }
            else
            {
                Debug.Log("Fire()");
                Fire();
            }
        }
    }
    public void Fire()
    {
        direction = (transform.position - initPos.position).normalized;
        rigidbody2D.linearVelocity = direction * _velocityArrow;
    }
    public void FireExpand()
    {
        direction = (initPos.position - centro.position).normalized;
        rigidbody2D.linearVelocity = direction * _velocityArrow;
    }
    private void OnEnable()
    {
        SuscribeEvents();
        Apagar(6f);
    }

    void SuscribeEvents()
    {    
        _attackPlayer.response += HideBullet;
    }
    private void OnDisable()
    {
        DesuscribeEvents();
        SetInitialValues();
    }
    void SetInitialValues()
    {
        if (initPos != null)
            transform.position = centro.position;
        if (target != null)
            transform.LookAt(target);

        _velocityArrow = velocityArrow;
        Kind = true;
    }
    void DesuscribeEvents()
    {       
        _attackPlayer.response -= HideBullet;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _velocityArrow = 0;
            _attackPlayer.OnEventRaise(damagebullet);
            HideBullet();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
    void Apagar(float timeToOff)
    {
        StartCoroutine(ApagarCorutine(timeToOff));
    }
    public IEnumerator ApagarCorutine(float time)
    {
        yield return new WaitForSecondsRealtime(time-1.5f);
        velocityArrow=velocityArrow * 2;
        yield return new WaitForSecondsRealtime(time);
        HideBullet();
    }
    void HideBullet()
    {
        gameObject.SetActive(false);
    }
}
