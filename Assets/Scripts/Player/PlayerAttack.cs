using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer weaponImage;
    public Player player;
    public GameObject weapon;
    public int damage=0; 
    void Start()
    {
        damage = player.damage;
    }
    public void Activettack()
    {
        StartCoroutine(ActiveWeaponCorutine());
    }
   
    IEnumerator ActiveWeaponCorutine()
    {
        ActiveWeapon();
        anim.SetTrigger("Attack");
        yield return new WaitForSecondsRealtime(0.2f);
        DesactiveWeapon();
    }
    void ActiveWeapon()
    {
        weaponImage.enabled = true;
        weapon.GetComponent<Collider2D>().enabled=true;
    }
    void DesactiveWeapon()
    {
        weaponImage.enabled = false;
        weapon.GetComponent<Collider2D>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Health tmp =collision.gameObject.GetComponent<Health>();
            tmp.UpdateHealth(damage);
        }
    }
}
