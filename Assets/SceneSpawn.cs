using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSpawn : MonoBehaviour
{
    public string spawnTag = "SpawnInicio"; // Puedes variar por escena o puerta

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject spawn = GameObject.FindWithTag(spawnTag);

        if (player != null && spawn != null)
        {
            player.transform.position = spawn.transform.position;
        }
    }
}
