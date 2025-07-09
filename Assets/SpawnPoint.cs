using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public string spawnID = "EntradaJefe"; 

    void Start()
    {
        string idEntrada = PlayerPrefs.GetString("SpawnID", "");

        if (idEntrada == spawnID)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = transform.position;
            }
        }
    }
}
