using TMPro;
using UnityEngine;

public class Puntos : MonoBehaviour
{

    private float puntos;

    private TextMeshProUGUI textMesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //puntos += Time.deltaTime;
        textMesh.text = puntos.ToString("SCORE: 0");
    }

    public void Sumarpuntos(float puntosEntrada)
    {
        puntos += puntosEntrada;
    }
}
