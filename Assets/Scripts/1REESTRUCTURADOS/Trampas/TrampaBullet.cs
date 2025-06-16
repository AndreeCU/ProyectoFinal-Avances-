using UnityEngine;

public class TrampaBullet : MonoBehaviour
{
    [Header("ValuesToFire")]
    public int spawnCooldown = 10;
    public int cantBullets = 2;
    public float intervalBullets =0.45f;
    private int currentCantBulletsFotFire;
    //public int maxBulletsOnScene = 4;
    public EnemyIa enemyIA;
    private Health playerHealth;
    private bool isPlayerArrive = false;
    private float spawnTimer = 0f;
    private float frameRateBullet = 0f;
    void Start()
    {
        currentCantBulletsFotFire = cantBullets;
        spawnTimer = spawnCooldown - 1;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnBulletsRate();
    }
    void SpawnBulletsRate()
    {
        if (isPlayerArrive)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnCooldown)
            {
                CreatedBullets();                
            }
        }

    }
    void CreatedBullets()
    {        
        if (playerHealth == null) return;

        frameRateBullet += Time.deltaTime;
        if (currentCantBulletsFotFire <= 0)
        {
            spawnTimer = 0f;
            currentCantBulletsFotFire = cantBullets;
        }
        else if (frameRateBullet >= intervalBullets)
        {
            EnemyIa tmp = Instantiate(enemyIA, transform.position, Quaternion.identity);
            tmp.SetHealthPlayer(playerHealth);
            tmp.SetTargetPlayer(playerHealth.transform);
            currentCantBulletsFotFire--;
            frameRateBullet = 0f;            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth = collision.gameObject.GetComponent<Health>();

            isPlayerArrive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerArrive = false;
        }
    }
}
