using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerNew player = other.GetComponent<PlayerNew>();
            if (player != null)
            {
                PlayerPrefs.SetFloat("CheckpointX", player.transform.position.x);
                PlayerPrefs.SetFloat("CheckpointY", player.transform.position.y);
                PlayerPrefs.SetFloat("CheckpointZ", player.transform.position.z);


                PlayerPrefs.SetInt("Puntos", player.puntos);

                player.RestaurarVidaCompleta();

                Debug.Log("Checkpoint activado y vida restaurada.");
            }
        }
    }
}
