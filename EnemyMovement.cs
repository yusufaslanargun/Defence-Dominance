using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // ========================================= //
    [Header("Utility Values")]

    [Tooltip("Movement speed of the enemy")]
    [SerializeField]
    float speed = 10f;
    // ========================================= //
    Transform endPoint;
    Transform target;
    int waypointIndex;


    private void Awake()
    {
        endPoint = GameObject.FindGameObjectWithTag("End").transform;
    }

    private void Start()
    {
        target = Waypoints.waypoints[0];
        waypointIndex = 0;
    }

    private void Update()
    {
        CheckEndPointDistance();
        SetDestination();
    }

    void SetDestination()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNewWaypoint();
        }
    }

    void CheckEndPointDistance()
    {
        if (Vector3.Distance(transform.position, endPoint.position) <= 0.2f)
        {
            Destroy(gameObject);
            GameManager.EnemyPassedLine();
            return;
        }
    }

    void GetNewWaypoint()
    {
        waypointIndex++;
        if (waypointIndex >= Waypoints.waypoints.Length)
        {
            target = endPoint;
        }
        else
        {
            target = Waypoints.waypoints[waypointIndex];
        }
    }

    public void SpeedUp()
    {
        speed = 100f;
    }
}
