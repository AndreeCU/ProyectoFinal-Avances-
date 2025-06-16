using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Health : MonoBehaviour
{
    public float characterHealth = 15;
    public float maxHealth;
    public bool isDeath => characterHealth <= 0;
    [Header("UI Character")]
    public Image HealthBar;


    public virtual void Awake()
    {
        maxHealth = characterHealth;

    }
    public virtual void Start()
    {
        UpdateCharacterUI();
    }
    public virtual void UpdateHealth(float damage)
    {
        characterHealth -= damage;
        if (characterHealth <= 0)
        {
            characterHealth = 0;
        }
        if (characterHealth > maxHealth)
        {
            characterHealth = maxHealth;
        }
        UpdateCharacterUI();
    }
    public virtual void UpdateHealth(int damage)
    {
        //Debug.Log("UpdateHealth int ");
        characterHealth -= damage;
        if (characterHealth <= 0)
        {
            characterHealth = 0;
        }
        if (characterHealth > maxHealth)
        {
            characterHealth = maxHealth;
        }
        UpdateCharacterUI();
    }
    public virtual void Desapear(float time)
    {
        gameObject.SetActive(false);
    }
    public virtual void UpdateCharacterUI()
    {
        //Debug.Log("UpdateCharacterUI int ");
        if (HealthBar == null) return;
        HealthBar.fillAmount = characterHealth / maxHealth;
    }
    public virtual void Death(float time)
    {
        gameObject.SetActive(false);
        //Debug.Log("Estoy muerto : "+gameObject.name);
    }

}