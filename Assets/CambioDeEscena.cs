using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    public string nombreEscenaDestino = "Nivel 3"; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerNew jugador = collision.GetComponent<PlayerNew>();
            if (jugador != null)
            {
                try
                {
                    PlayerPrefs.SetInt("Puntos", jugador.puntos);
                    PlayerPrefs.Save();
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error guardando puntos antes del cambio de escena: " + e.Message);
                }

                SceneManager.LoadScene(nombreEscenaDestino);
            }
        }
    }
}
