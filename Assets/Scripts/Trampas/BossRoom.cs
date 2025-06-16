using System.Collections;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public GameObject boss;
    public Player _player;
    public GameEventListeners inRoom;
    public float _timeToWait;
    public AudioSource Sound;
    private void Start()
    {        
        inRoom.response += Admirar;
        inRoom.response += OffCollision;
        inRoom.response += VoiceBoss;
        inRoom.response += Active;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //_player = GetComponent<Player>();
            inRoom.OnEventRaise();
        }
    }
    void Active()
    {
        boss.SetActive(true);
    }
    void Admirar()
    {
        StartCoroutine(AdmirarCorutine());
    }
    IEnumerator AdmirarCorutine()
    {
        _player.DesactivePlayer();
     
        yield return new WaitForSecondsRealtime(_timeToWait);
     
        _player.ActivePlayer();
    }
    void OffCollision()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
    void VoiceBoss()
    {
        Sound.Play();
    }
}
