using UnityEngine;
using System;
using System.Collections;

public class BossMovement : MonoBehaviour
{
    public float dashSpeed = 10f;
    public float blinkCooldown = 3f;
    public float dashDuration = 0.2f;
    [SerializeField] private Transform areaTransform;

    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float minY = -3f;
    [SerializeField] private float maxY = 3f;
    private Vector2 targetPosition;
    private bool isDashing = false;

    public float moveSpeed = 2f;
    public float waitTime = 2f;           
    public float minDistance = 1f;
    public bool isMoving = false;
    void Start()
    {
      
        StartCoroutine(WanderRoutine());
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            //Debug.Log(Vector2.Distance(transform.position, targetPosition));
            if (Vector2.Distance(transform.position, targetPosition) <= minDistance)
            {
                isMoving = false;
            }
        }
    }


    IEnumerator WanderRoutine()
    {
       
            if (!isMoving)
            {
                targetPosition = GetRandomPositionFromAreaTransform();
                isMoving = true;
            }
        //Debug.Log(targetPosition);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(WanderRoutine());

    }

    Vector2 GetRandomPositionFromAreaTransform()
    {
        Vector2 center = areaTransform.position;
        Vector2 size = areaTransform.localScale; // usa la escala como tamaño del área

        float x = UnityEngine.Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float y = UnityEngine.Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);

        return new Vector2(x, y);
    }

    void SetRandomTarget()
    {
        Vector2 randomPos = GetRandomPositionFromAreaTransform();
        targetPosition = randomPos;
    }

    public void Blink()
    {
        transform.position = GetRandomPositionFromAreaTransform();
    }

    public void Evade(Vector2 dangerPoint, float evadeDistance = 3f)
    {
        Vector2 dir = (Vector2)transform.position - dangerPoint;
        dir.Normalize();
        Vector2 evadeTarget = (Vector2)transform.position + dir * evadeDistance;

        StartCoroutine(DashTo(evadeTarget));
    }

    public void DashToPoint(Vector2 point)
    {
        StartCoroutine(DashTo(point));
    }

    private IEnumerator DashTo(Vector2 destination)
    {
        isDashing = true;
        float time = 0f;

        Vector2 startPos = transform.position;

        while (time < dashDuration)
        {
            transform.position = Vector2.Lerp(startPos, destination, time / dashDuration);
            time += Time.deltaTime * dashSpeed;
            yield return null;
        }

        transform.position = destination;
        isDashing = false;
    }
    // Gizmos para mostrar el punto objetivo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPosition);
        Gizmos.DrawSphere(targetPosition, 0.2f);
    }
}
