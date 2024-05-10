using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ========================================= //
    [Header("Stats")]

    [Tooltip("Enemy Health Point")]
    [SerializeField]
    int health = 100;

    [Tooltip("How money will gain when this enemy dies")]
    [SerializeField]
    int value = 50;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += value;
        PlayerPrefs.SetInt("enemyKilled", PlayerPrefs.GetInt("enemyKilled") + 1);
        Destroy(gameObject);
    }
}
