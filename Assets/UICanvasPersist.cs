using UnityEngine;

public class UICanvasPersist : MonoBehaviour
{
    private static UICanvasPersist instancia;

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
    }
}
