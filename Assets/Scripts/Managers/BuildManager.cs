using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    [Header("Prefabs")]
    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;
    public bool CanBuild { get { return turretToBuild!= null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public GameObject GetSelectedTurret() => turretToBuild.prefab;

    public void BuildTurretOnNode(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough gold!");
            return;
        }
        else
        {
            PlayerStats.RemoveMoney(turretToBuild.cost);
        }

        GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        GameObject buildEffectInstance = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
        Destroy(buildEffectInstance, 1.5f);
    }
}
