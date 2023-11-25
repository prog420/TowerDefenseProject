using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject canvasUI;

    public TMP_Text upgradeCost;
    public Button upgradeButton;

    public void SetTarget(Node _target)
    {
        this.target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = $"${target.turretBlueprint.upgradeCost}";
            upgradeButton.interactable = true;
        } else
        {
            upgradeCost.text = "MAX LEVEL";
            upgradeButton.interactable = false;
        }

        canvasUI.SetActive(true);
    }

    public void Hide()
    {
        canvasUI.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }
}
