using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
public class Cañon1 : MonoBehaviour
{
    public BossLight bossLight;
    public Bullets _bullet;
    public int cantBullet=30;
    public int canOleadas=12;
    public float candenceBullet=0.7f;
    private Bullets[] _bulletArray;
    private Transform target;
    public bool inSide;
    private Player _player;
    [Header("Rotacion")]
    public float velocidadAngular = 90f;
    public float velocidadExpansion = 1f;
    private float angulo;
    private float radio;
    void Start()
    {
        _bulletArray = new Bullets[cantBullet];
        CreatedArrowArray();
       
    }
    private void OnEnable()
    {
        SuscribeEvents();
    }
    void SuscribeEvents()
    {
        //bossLight.Attack1.response += CandenciaDisparo;
    }
    private void OnDisable()
    {
        DesuscribeEvents();
    }
    void DesuscribeEvents()
    {
        //bossLight.Attack1.response -= CandenciaDisparo;
    }
    private void CreatedArrowArray()
    {
        for (int i = 0; i < cantBullet; i++)
        {
            Bullets tmp = Instantiate(_bullet, transform.position, Quaternion.identity);
            tmp.SetCentro(bossLight.transform);
            tmp.SetInitPos(bossLight.transform);
            tmp.SetDamage(bossLight._attack);
            tmp.gameObject.SetActive(false);
            Direction(tmp);
            _bulletArray[i] = tmp;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player = collision.gameObject.GetComponent<Player>();
            //inSide = true;
            //SearchPlayer(collision.gameObject);
            //_player._playerEvents.Knockback.OnEventRaise();
        }
        if (collision.gameObject.tag == "Weapon")
        {
            Debug.Log("chocarArma");
            //Player player = collision.gameObject.GetComponent<Player>();
            //bossLight.UpdateCuLife.OnEventRaise(_player.damage);

        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        //inSide = false;
    //        StopCoroutine(CandenciaDisparoCorutine());
    //    }
    //}

    private void Direction(Bullets _bullet)
    {
        if(target!=null)
            _bullet.direction = (target.position - transform.position).normalized;
    }
    private void SearchPlayer(GameObject gameObject)
    {
        target = gameObject.transform;
    }
    public void CandenciaDisparo()
    {
        Debug.Log("CandenciaDisparo");
        StartCoroutine(CandenciaDisparoCorutine());
    }
    IEnumerator CandenciaDisparoCorutine()
    {
        for (int i = 0; i < canOleadas; i++)
        {
            _bulletArray[i].gameObject.SetActive(true);            
            Direction(_bulletArray[i]);
            yield return new WaitForSecondsRealtime(candenceBullet);
        }

    }
    private void Update()
    {
        MovementCircularInEje();
    }
    void MovementCircularInEje()
    {
        angulo += velocidadAngular * Time.deltaTime;

        float rad = angulo * Mathf.Deg2Rad;

        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);

        // Rotación sobre su propio eje
        transform.rotation = Quaternion.Euler(0f, 0f, angulo);

    }
}
