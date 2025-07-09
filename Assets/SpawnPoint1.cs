using UnityEngine;

public class SpawnPoint1 : MonoBehaviour
{
    public string spawnID = "PuertaRegreso";

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
