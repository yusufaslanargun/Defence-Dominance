using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // ========================================= //
    [Header("Turret Blueprints")]

    [SerializeField]
    TurretBlueprint turret1;
    [SerializeField]
    TurretBlueprint missile1;
    // ========================================= //
    [Header("Costs")]

    [SerializeField]
    Text turret1Cost;
    [SerializeField]
    Text missile1Cost;
    // ========================================= //

    BuildManager buildManager;
    bool isActive = true;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    private void Update()
    {
        TextFieldDisplays();
    }

    void TextFieldDisplays()
    {
        turret1Cost.text = "$" + turret1.cost.ToString();
        missile1Cost.text = "$" + missile1.cost.ToString();
    }
    public void PurchaseTurret1()
    {
        buildManager.SelectTurretToBuild(turret1);
    }

    public void PurchaseMissileLauncher()
    {
        buildManager.SelectTurretToBuild(missile1);
    }

    public void HideShop()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
        buildManager.ResetTurretToBuild();
    }
}
