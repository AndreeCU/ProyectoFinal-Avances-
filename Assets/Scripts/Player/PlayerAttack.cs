using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Referencias públicas (asignables desde Inspector)
    [Header("Referencias")]
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private Collider2D weaponCollider;
    [SerializeField] private Player player;

    [Header("Configuración")]
    [SerializeField] private float activeTime = 0.2f; // Tiempo que el arma permanece activa
    #endregion

    private Animator anim;
    private int damage;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        if (player == null)
            player = GetComponent<Player>();

        if (weaponSprite == null)
            Debug.LogWarning("[PlayerAttack] Falta asignar weaponSprite", this);
        if (weaponCollider == null)
            weaponCollider = GetComponentInChildren<Collider2D>(true);

        if (weaponCollider != null)
            weaponCollider.enabled = false;
    }

   // private void Start()
   // {
   //     damage = player != null ? Player.damage : 1;
   // }

    // Llamado por PlayerPowerStates u otros scripts para realizar un ataque
    public void PerformAttack() => Attack();

    public void Attack()
    {
        if (!gameObject.activeInHierarchy) return;
        StopAllCoroutines();
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        EnableWeapon();
        if (anim != null) anim.SetTrigger("Attack");
        yield return new WaitForSeconds(activeTime);
        DisableWeapon();
    }

    private void EnableWeapon()
    {
        if (weaponSprite != null) weaponSprite.enabled = true;
        if (weaponCollider != null) weaponCollider.enabled = true;
    }

    private void DisableWeapon()
    {
        if (weaponSprite != null) weaponSprite.enabled = false;
        if (weaponCollider != null) weaponCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!weaponCollider.enabled) return; // Solo si el arma está activa
        if (other.CompareTag("Enemy"))
        {
            Health h = other.GetComponent<Health>();
            if (h != null)
                h.UpdateHealth(damage);
        }
    }
}
