using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // ========================================= //
    [Header("References")]

    [Tooltip("Reference to the rotating part of turret")]
    [SerializeField]
    Transform partToRotate;

    [Tooltip("Reference to bullet prefab")]
    [SerializeField]
    GameObject bulletPrefab;

    [Tooltip("Reference to where bullet will come")]
    [SerializeField]
    Transform firePoint;

    [Tooltip("Reference to the tag of enemy")]
    [SerializeField]
    string enemyTag = "Enemy";
    // ========================================= //
    [Header("Utility Values")]

    [Tooltip("Damage of the turret")]
    [SerializeField]
    int damage = 50;

    [Tooltip("Range of the turret")]
    [SerializeField]
    float range = 15f;

    [Tooltip("Rotating speed of the turret while swapping between targets")]
    [SerializeField]
    float rotateSpeed = 10f;

    [Tooltip("How many bullets will be fired each second")]
    [SerializeField]
    float fireRate = 1f;

    // ========================================= //
    private Transform target;
    private float fireCountdown = 0f;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }
        AdjustRotation();
        FireCountdown();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closestRange = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestRange)
            {
                closestRange = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && closestRange <= range)
        {
            target = closestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void AdjustRotation()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void FireCountdown()
    {
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.SetDamage(damage);

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
