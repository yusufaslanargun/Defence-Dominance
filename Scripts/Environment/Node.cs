using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    // ========================================= //
    [Header("References")]

    [Tooltip("Color for hover")]
    [SerializeField]
    Color hoverColor;

    [Tooltip("Color when don't have enough money for turret")]
    [SerializeField]
    Color notEnoughMoneyColor;
    // ========================================= //
    [Header("Utility Values")]

    [Tooltip("Offset for building new turret")]
    [SerializeField]
    Vector3 positionOffset;
    // ========================================= //
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;
    // ========================================= //

    Renderer rend;
    Color defaultColor;
    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        buildManager = BuildManager.instance;
        defaultColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }
        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }
    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.turretPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        Debug.Log("Turret Purchased!");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        isUpgraded = true;

        Debug.Log("Turret Upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;

        Debug.Log("Turret Sold!");
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.CanBuild)
        {
            return;
        }

        rend.material.color = (buildManager.HasMoney) ? hoverColor : notEnoughMoneyColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = defaultColor;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
