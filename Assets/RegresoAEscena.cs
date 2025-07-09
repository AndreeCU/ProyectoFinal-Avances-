using UnityEngine;
using UnityEngine.SceneManagement;

public class RegresoAEscena : MonoBehaviour
{
    public string nombreEscenaDestino = "Nivel 2"; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerNew jugador = collision.GetComponent<PlayerNew>();

            if (jugador != null)
            {
                PlayerPrefs.SetInt("Puntos", jugador.puntos);
                PlayerPrefs.SetString("SpawnID", "PuertaRegreso"); 
                SceneManager.LoadScene(nombreEscenaDestino);
            }
        }
    }
}
