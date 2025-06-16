using UnityEngine;

public class SpawmEnemyIA : MonoBehaviour
{
    [Header("ValuesToSpawn")]
    public int spawnCooldown = 3;
    public int enemiesOnScene = 0;
    public int maxEnemiesOnScene = 6;
    //public float timeToSpawn = 5;
    public EnemyIa enemyIA;
    public bool isPlayerArrive = false;
    public bool isWorkSpawn = true;
    public bool isNormalTime = true;
    public Health playerHealth;
    //public float frameRate = 0;
    private int enemiesLeftToSpawn = 0;
    private float spawnTimer = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemiesRate();
    }
    void SpawnEnemiesRate()
    {

        if (isPlayerArrive)
        {
            if (enemiesOnScene < maxEnemiesOnScene)
            {
                spawnTimer += Time.deltaTime;

                if (spawnTimer >= spawnCooldown)
                {
                    spawnTimer = 0f;
                    CreatedEnemys();
                    enemiesOnScene++;
                }
            }
        }
        
    }
    void CreatedEnemys()
    {
        if (playerHealth == null) return;
        EnemyIa tmp = Instantiate(enemyIA, GetPointOutsideScreen(1f),Quaternion.identity);
        
        tmp.SetHealthPlayer(playerHealth);
        tmp.SetTargetPlayer(playerHealth.transform);
    }
    Vector3 GetPointOutsideScreen(float offset)
    {
        Camera cam = Camera.main;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        // Elige un borde aleatorio: 0 = arriba, 1 = abajo, 2 = izquierda, 3 = derecha
        int border = Random.Range(0, 4);

        Vector3 spawnPosition = Vector3.zero;

        switch (border)
        {
            case 0: // Top
                spawnPosition = cam.ScreenToWorldPoint(new Vector3(Random.Range(0, screenSize.x), screenSize.y + offset, cam.nearClipPlane));
                break;
            case 1: // Bottom
                spawnPosition = cam.ScreenToWorldPoint(new Vector3(Random.Range(0, screenSize.x), -offset, cam.nearClipPlane));
                break;
            case 2: // Left
                spawnPosition = cam.ScreenToWorldPoint(new Vector3(-offset, Random.Range(0, screenSize.y), cam.nearClipPlane));
                break;
            case 3: // Right
                spawnPosition = cam.ScreenToWorldPoint(new Vector3(screenSize.x + offset, Random.Range(0, screenSize.y), cam.nearClipPlane));
                break;
        }

        spawnPosition.z = 0; // 2D
        return spawnPosition;
    }

   
    public void StopSpawnPower()
    {
        isNormalTime = false;
    }
    public void ContinueSpawnPower()
    {
        isNormalTime = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
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
