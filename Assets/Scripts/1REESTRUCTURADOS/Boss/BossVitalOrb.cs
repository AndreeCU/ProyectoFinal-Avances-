using UnityEngine;

public class BossVitalOrb : MonoBehaviour
{
    public GameEvents updateBossLife;
    private Player player;
    public bool isActive;
    [Header("Bullets")]
    public Orb orbIA;
    //public int _isOver;
    [Header("Rotacion")]
    public float velocidadAngular = 90f;
    public float velocidadExpansion = 1f;
    private float angulo;
    [Header("ValuesToFire")]
    public bool _fire = false;
    public int Id=0;

    public ValuesToFire circularFire;
    public ValuesToFire awayFire;
    public ValuesToFire targetFire;
    public float spawnTimer;
    public float frameRateBullet;
    private void Start()
    {
        circularFire.currentCantBulletsFotFire = circularFire._cantBullets;
        spawnTimer = circularFire._timeReload - 1;
    }
    public void setPlayer(Player player)
    {
        this.player = player;
    }
   
    private void Update()
    {
        SpawnOrbsRate();
        MovementCircularInEje();
    }
    void SpawnOrbsRate()
    {
        if (isActive)
        {
            if (_fire)
            {
                spawnTimer += Time.deltaTime;

                switch (Id)
                {
                    case 0:
                        if (spawnTimer >= circularFire._timeReload)
                        {
                            //circularFire._timeReload = circularFire._timeReload + (circularFire.intervalBullets * circularFire._cantBullets);
                            CreatedOrbs(circularFire, Id);
                        }
                        break;

                    case 1:
                        if (spawnTimer >= awayFire._timeReload)
                        {
                            //awayFire._timeReload = awayFire._timeReload + (awayFire.intervalBullets * awayFire._cantBullets);
                            CreatedOrbs(awayFire, Id);
                        }
                        break;

                    case 2:
                        if (spawnTimer >= targetFire._timeReload)
                        {
                            //targetFire._timeReload = targetFire._timeReload + (targetFire.intervalBullets * targetFire._cantBullets);
                            CreatedOrbs(targetFire, Id);
                        }
                        break;
                }

            }
        }              
    }
    void CreatedOrbs(ValuesToFire kindFire,int Id)
    {
        //if (playerHealth == null) return;

        frameRateBullet += Time.deltaTime;
        if (circularFire.currentCantBulletsFotFire <= 0)
        {
            spawnTimer = 0f;
            circularFire.currentCantBulletsFotFire = circularFire._cantBullets;
        }
        else if (frameRateBullet >= circularFire.intervalBullets)
        {
            Orb tmp = Instantiate(orbIA, transform.position, Quaternion.identity);
            tmp.centro = transform;
            tmp.enemyIa.targetPlayer =player.transform;
            tmp.enemyIa.targetPlayer = player.transform;
            tmp.Kind = Id;
            circularFire.currentCantBulletsFotFire--;
            frameRateBullet = 0f;
        }
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
   //private void OnTriggerEnter2D(Collider2D collision)
   //{
   //    if (collision.gameObject.tag == "Weapon")
   //    {
   //        if (player != null)
   //            updateBossLife.Raise((float)player.damage);
   //        else
   //            Debug.Log("player is null");
   //    }
   //}
    [System.Serializable]
    public struct ValuesToFire
    {
        public float _timeReload ;
        public int _cantBullets ;
        public float intervalBullets ;
        public int currentCantBulletsFotFire;
        
    }
}
