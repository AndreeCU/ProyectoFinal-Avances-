using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System;
public class SpawmEnemys : MonoBehaviour
{
    public Transform player;
    [Header("DataEnemys")]
    public Enemy enemyPref;
    public int cantUnits;
    private Enemy[] units;
    public bool inSide;
    public float TimeToNectStage = 8;
    private float frameRate = 0;
    void Start()
    {
        units = new Enemy[cantUnits];
        CreateUnits();
    }
    void CreateUnits()
    {
        for (int i = 0; i < units.Length; i++)
        {
            Enemy tmp = Instantiate(enemyPref, transform.position, Quaternion.identity);
            tmp.player = player;
            tmp.gameObject.SetActive(false);
            units[i] = tmp;
        }
    }

    private void Update()
    {
        frameRate += Time.deltaTime;
        if(frameRate>= TimeToNectStage&& inSide)
        {
            StartCoroutine(SpameoTiempo(units));
            frameRate = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inSide = true;
            //StartCoroutine(SpameoTiempo(units));

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inSide = false;
            //StopCoroutine(SpameoTiempo(units));
        }
    }
    IEnumerator SpameoTiempo(Enemy[] units)
    {
        for (int i = 0; i < units.Length; i++)
        {
            int rdn = UnityEngine.Random.Range(0, 5);
            units[i].SetInitPos(transform);
            if (inSide)
            {
                units[i].gameObject.SetActive(true);
           
                yield return new WaitForSecondsRealtime(rdn);
            }
            else
            {
                yield return null;

            }

        }

    }
  
}
