using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Header("Managers")]
    private BuildManager buildManager;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    [Header("Node Settings")]
    [SerializeField] private Vector3 buildPositionOffset = new Vector3(0f, 0.5f, 0f);
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color cantbuildColor;
    private Color startColor;
    private Renderer rend;

    public Vector3 GetBuildPosition()
    {
        return transform.position + buildPositionOffset;
    }

    private void Start()
    {
        buildManager = BuildManager.Instance;

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough gold!");
            return;
        }
        else
        {
            PlayerStats.RemoveMoney(blueprint.cost);
        }

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        turretBlueprint = blueprint;

        GameObject buildEffectInstance = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffectInstance, 1.5f);

        Debug.Log("Turret build!");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough gold!");
            return;
        }
        else
        {
            PlayerStats.RemoveMoney(turretBlueprint.upgradeCost);
        }

        // Get rid of the old turret
        Destroy(turret);

        // Build a new one
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject buildEffectInstance = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffectInstance, 1.5f);

        isUpgraded = true;

        Debug.Log("Turret upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.AddMoney(turretBlueprint.GetSellCost());
        Destroy(turret);
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Node is already taken by another turret
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        // Player don't have any selected turret - do not build turret
        if (!buildManager.CanBuild)
            return;

        // Build a turret
        BuildTurret(buildManager.GetTurretToBuild());

    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Player don't have any selected turret - do not highlight the node
        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney && turret == null)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = cantbuildColor;
        }

    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
