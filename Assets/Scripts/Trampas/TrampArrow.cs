using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class TrampArrow : MonoBehaviour
{
    public Arrow arrow;
    public int cantArrow;
    public float candenceArrow;
    private Arrow[] arrowArray;
    private Transform target;
    public bool inSide;
    void Start()
    {
        arrowArray = new Arrow[cantArrow];
        CreatedArrowArray();
    }

    private void CreatedArrowArray()
    {
        for (int i= 0; i< arrowArray.Length; i++)
        {
            Arrow tmp = Instantiate(arrow, transform.position, Quaternion.identity);
            tmp.gameObject.SetActive(false);
            arrowArray[i] = tmp;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Console.WriteLine("OnTriggerEnter2D");
            inSide = true;
            SearchPlayer(collision.gameObject);

            StartCoroutine(CandenciaDisparo(arrowArray));

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inSide = false;
            StopCoroutine(CandenciaDisparo(arrowArray));
        }
    }
    private void Direction(Arrow arrow)
    {

        arrow.direction = (target.position - transform.position).normalized;
    }
    private void SearchPlayer(GameObject gameObject )
    {
        target = gameObject.transform;
        //Console.WriteLine("OnTriggerEnter2D");
    }
    IEnumerator CandenciaDisparo(Arrow[] arrow)
    {
        for (int i = 0; i < arrow.Length; i++)
        {
            arrow[i].SetInitPos(transform);
            if (inSide)
            {
                //Console.WriteLine("CandenciaDisparo");
                arrow[i].gameObject.SetActive(true);
                Direction(arrow[i]);
                //Console.WriteLine("EndCandenciaDisparo" + i);
                yield return new WaitForSecondsRealtime(candenceArrow);
            }
            else
            {
                yield return null;
                //Console.WriteLine("End");
                //Console.WriteLine("End+++++++" + i);
            }
           
        }
    }
}
