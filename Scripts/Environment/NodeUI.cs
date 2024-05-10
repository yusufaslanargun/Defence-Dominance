using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [SerializeField]
    Text upgradeCost;

    [SerializeField]
    Button upgradeButton;

    [SerializeField]
    Text sellCost;

    public GameObject ui;

    Node target;

    public void SetTarget(Node node)
    {
        target = node;

        transform.position = target.GetBuildPosition();

        ui.SetActive(true);

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost.ToString();
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellCost.text = "$" + target.turretBlueprint.GetSellAmount().ToString();
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
