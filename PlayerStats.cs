using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    // ========================================= //
    [Header("Values")]

    [Tooltip("Money amount that player will start with")]
    [SerializeField]
    int startMoney = 400;

    private void Start()
    {
        Money = startMoney;
    }

}
