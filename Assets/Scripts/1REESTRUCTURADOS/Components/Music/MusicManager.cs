using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip mainTheme;
    public AudioClip battleTheme;
    public AudioClip lostTheme;
    [Header("Events")]
    public GameEventListeners roomBoss;
    public GameEventListeners gameOver;
    public float _timeToWait;
    private void OnEnable()
    {
        gameOver.response += GameOver;
        roomBoss.response += GameBattle;
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    void GameOver()
    {
        PlayMusic(lostTheme);
    }
    void GameBattle()
    {
        PlayMusic(battleTheme);
    }
}
