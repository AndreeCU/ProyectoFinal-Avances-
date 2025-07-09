using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaJefeFinal : MonoBehaviour
{
    public string nombreEscenaJefe = "EscenaJefeFinal";
    public string spawnID = "EntradaJefe";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerNew jugador = collision.GetComponent<PlayerNew>();

            if (jugador != null && jugador.puntos >= 500)
            {
                PlayerPrefs.SetString("SpawnID", spawnID); 
                PlayerPrefs.SetInt("Puntos", jugador.puntos);
                SceneManager.LoadScene("SegundoPiso");
            }
            else
            {
                Debug.Log("Necesitas al menos 500 puntos para entrar al jefe final.");
            }
        }
    }
}
