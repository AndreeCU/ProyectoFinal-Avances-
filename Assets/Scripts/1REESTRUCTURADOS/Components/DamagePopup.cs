using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI damageText;

    public void Setup(int damage)
    {        
        damageText.text = damage.ToString();
    }

    public void DestroyAfter(float time)
    {
        Destroy(gameObject, time);
    }
}
