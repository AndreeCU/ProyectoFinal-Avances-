using UnityEngine;

public class Orb : MonoBehaviour
{
    public EnemyIa enemyIa;
    public DamagePopup damagePopup;
    private int damagebullet=13;
    private float _velocityOrb;
    public int Kind;
    [Header("Rotacion")]
    public Transform centro;
    private Transform initPos;
    public float velocidadAngular = 90f;
    public float velocidadExpansion = 1f;
    public float radioInicial = 1f;
    private float angulo;
    private float radio;
    private float framerate = 0;
    public float timeToExpand = 4;
    void Start()
    {
        radio = radioInicial;
        _velocityOrb = enemyIa.speed;
        initPos = transform;
    }
    public void SetInitPos(Transform init)
    {
        initPos = init;
    }
 
    public void SetCentro(Transform centro)
    {
        this.centro = centro;
    }
    // Update is called once per frame
    void Update()
    {
        switch (Kind)
        {
            case 0:
                MovementCircularSpheres();
                break;
        }
        framerate += Time.deltaTime;
        MovementCircularInEje();
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
        switch (Kind)
        {
            case 1:
                if (framerate <= timeToExpand)
                {
                    Debug.Log("FireExpand()");
                    FireExpand();
                }               
                break;
            case 2:
                if (framerate <= timeToExpand)
                {
                    Debug.Log("FireExpand()");
                    Fire();
                }
                break;
        }  

    }
    public void Fire()
    {
        enemyIa.direction = (enemyIa.targetPlayer.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().linearVelocity = enemyIa.direction * _velocityOrb;
    }
    public void FireExpand()
    {
        enemyIa.direction = (initPos.position - centro.position).normalized;
        GetComponent<Rigidbody2D>().linearVelocity = enemyIa.direction * _velocityOrb;
    }
    void PrintDamage()
    {
        Vector3 hitPosition = enemyIa.targetPlayer.position;
        DamagePopup popup = Instantiate(damagePopup, hitPosition, Quaternion.identity);
        popup.Setup(enemyIa.damage);
        popup.DestroyAfter(0.75f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {           
            PrintDamage();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    void MovementCircularInEje()
    {
        angulo += (velocidadAngular*1.2f) * Time.deltaTime;

        float rad = angulo * Mathf.Deg2Rad;

        float x = Mathf.Cos(rad);
        float y = Mathf.Sin(rad);

        // Rotación sobre su propio eje
        transform.rotation = Quaternion.Euler(0f, 0f, angulo);

    }
}
