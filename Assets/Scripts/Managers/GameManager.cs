using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public BossEvents boss;
    public PlayerEvents _playerEvents;
    public SceneLoadManager sceneLoadManager;
    [Header("EventsActivador")]
    public GameEventListeners ChangeSceneD;
    void Start()
    {
        _playerEvents = player.GetComponent<PlayerEvents>();
       
     
    }
    private void OnEnable()
    {
        _playerEvents.Died.response += ChangeScene;
        boss.Disappear.response += ChangeScene;
    }
    private void OnDisable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    void ChangeScene()
    {
        //ChangeSceneD.OnEventRaise();
        sceneLoadManager.ChangeScene();
    }
}
