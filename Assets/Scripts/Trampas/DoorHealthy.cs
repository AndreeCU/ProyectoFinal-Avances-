using UnityEngine;
using UnityEngine.UI;

public class DoorHealthy : MonoBehaviour
{
    public float health =1;
    private float healthInit = 1;
    public float maxHealth = 100;
    //public player
    public float timeCast = 6;
    public float timeDecreseCast = 18;
    public bool onSide;
    public bool isReady => health >= maxHealth;

    public Slider healthSlider;
    private void Start()
    {
        healthInit = health;
    }
    private void Update()
    {
        UpdateHealth();
        UpdateHealthLess();
        if (isReady)
        {
            // se abre;
        }
    }
    void UpdateHealth()
    {
        if (onSide)
        {
            if (health < maxHealth)
            {
                float healRate = (maxHealth - healthInit) / timeCast;
                health += (healRate * Time.deltaTime);
                UpdateSlider();
                if (health >= maxHealth)
                {
                    health = maxHealth;
                }
            }
        }
    
    }
    void UpdateHealthLess()
    {
        if (!onSide)
        {          
                float healRate = (maxHealth - healthInit) / timeDecreseCast;
                health -= (healRate * Time.deltaTime);
                UpdateSlider();
                if (health <= 0)
                {
                    health = 0;
                }
            
        }

    }
    private void UpdateSlider()
    {
        healthSlider.value = health/ maxHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            onSide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onSide = false;

        }
    }

}
