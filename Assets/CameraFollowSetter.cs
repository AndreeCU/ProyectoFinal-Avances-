using Unity.Cinemachine;
using UnityEngine;

public class CameraFollowSetter : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(EsperarYAsignarPlayer());
    }

    private System.Collections.IEnumerator EsperarYAsignarPlayer()
    {
        GameObject player = null;

        while (player == null)
        {
            player = GameObject.FindWithTag("Player");
            yield return null;
        }

        if (vcam != null && player != null)
        {
            vcam.Follow = player.transform;
        }
    }
}
