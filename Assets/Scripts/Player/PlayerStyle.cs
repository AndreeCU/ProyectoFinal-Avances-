using UnityEngine;
using UnityEngine.UI;

public class PlayerStyle : MonoBehaviour
{

    [Header("Cambio de Color")]
    public Image[] partsBody;
    private void Start()
    {
        ChangeColor( Color.yellow);

    }
    public void ChangeColor(Color newColor)
    {
        for (int i = 0; i < partsBody.Length; i++)
        {
            partsBody[i].color = newColor;
        }
    }
}
