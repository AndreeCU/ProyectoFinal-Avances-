using System;
using System.Collections;
using UnityEngine;
public class Arrow : MonoBehaviour
{
    public float velocityArrow;
    private float _velocityArrow;
    public Vector3 direction;
    public Transform target;
    private Rigidbody2D rigidbody2D;
    private Transform initPos;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Fire();
        transform.rotation = Quaternion.identity;
    }
    public void Fire()
    {
        rigidbody2D.linearVelocity = direction * _velocityArrow;
    }
    private void OnEnable()
    {
        _velocityArrow = velocityArrow;
        StartCoroutine(Apagar(6f));
        if(initPos!=null)
            transform.position = initPos.position;
        if (target != null)
            transform.LookAt(target);
    }
    public IEnumerator Apagar(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 0)
        {
            _velocityArrow = 0;
            gameObject.transform.SetParent(collision.gameObject.transform);
            StartCoroutine(Apagar(2f));
        }
    }
    public void SetInitPos(Transform init)
    {
        initPos = init;
    }
}
