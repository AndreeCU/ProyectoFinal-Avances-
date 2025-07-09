using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public int cantidadEnemigos = 3;
    public float intervalo = 1.5f;
    public bool autoSpawnAlInicio = true;

    private void Start()
    {
        if (autoSpawnAlInicio)
        {
            InvokeRepeating(nameof(SpawnEnemigo), 0f, intervalo);
        }
    }

    public void SpawnEnemigo()
    {
        if (cantidadEnemigos <= 0)
        {
            CancelInvoke(nameof(SpawnEnemigo));
            return;
        }

        Instantiate(enemigoPrefab, transform.position, Quaternion.identity);
        cantidadEnemigos--;
    }
}
