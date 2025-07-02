using UnityEngine;
using UnityEngine.Timeline;
using static UnityEngine.EventSystems.EventTrigger;

public class BossLight : Health
{   
    public int _attack;
    [Header("Components")]
    public BossVitalOrb[] vitalOrb;
    public Player _player;

    [Header("Victory UI")]
    public GameObject victoryPanel;

    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
        UpdateVitalOrbs();


    }
    void UpdateVitalOrbs()
    {
        for(int i = 0; i< vitalOrb.Length; i++)
        {
            vitalOrb[i].setPlayer(_player);
        }
    }
    public override void UpdateHealth(float damage)
    {
        base.UpdateHealth(damage);
      
    }
    public override void UpdateCharacterUI()
    {
        base.UpdateCharacterUI();
    }
    public override void Death(float time)
    {
        base.Death(time);

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0f; 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public override void Desapear(float time)
    {
        base.Desapear(time);
    }

  
}
