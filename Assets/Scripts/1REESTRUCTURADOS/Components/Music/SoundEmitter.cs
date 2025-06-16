using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public AudioClip clip;

    void Start()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        Destroy(gameObject, clip.length); 
    }
}