using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private PlayerNew playerController;
    private float vidaMaxima;

    void Start()
    {
        GameObject jugadorGO = GameObject.FindWithTag("Player");
        if (jugadorGO != null)
        {
            playerController = jugadorGO.GetComponent<PlayerNew>();
            vidaMaxima = playerController.maxHealth;
        }
        else
        {
            Debug.LogError("No se encontró un GameObject con tag 'Player'.");
        }
    }

    void Update()
    {
        if (playerController == null || rellenoBarraVida == null) return;

        rellenoBarraVida.fillAmount = playerController.CurrentHealth / vidaMaxima;
    }
}
