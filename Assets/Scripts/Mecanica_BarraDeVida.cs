using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mecanica_BarraDeVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    public Health playerControl;
    private float vidaMaxima;

    void Start()
    {
        vidaMaxima = playerControl.characterHealth;

    }
    void Update()
    {
        rellenoBarraVida.fillAmount = playerControl.characterHealth / vidaMaxima;
    }
}
