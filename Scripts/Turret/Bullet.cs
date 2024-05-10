using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ========================================= //
    [Header("References")]

    [Tooltip("Reference for bullet impact effect")]
    [SerializeField]
    GameObject bulletEffect;
    // ========================================= //
    [Header("Utility Values")]

    [Tooltip("Speed of the bullet")]
    [SerializeField]
    float speed = 70f;

    [Tooltip("Radius of the explosion")]
    [SerializeField]
    float explosionRadius = 0f;
    // ========================================= //

    Transform target;
    private int damage = 0;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        BulletTrajectory();
    }

    void BulletTrajectory()
    {
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    public void Seek(Transform newTarget)
    {
        target = newTarget;
    }

    public void HitTarget()
    {
        GameObject impactIns = (GameObject)Instantiate(bulletEffect, transform.position, transform.rotation);
        Destroy(impactIns, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }
    void Explode()
    {
        Collider[] objectsHit = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in objectsHit)
        {
            if (collider.CompareTag("Enemy"))
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy enemyHealth = enemy.GetComponent<Enemy>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
