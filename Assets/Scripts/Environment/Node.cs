using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Header("Managers")]
    private BuildManager buildManager;

    [Header("Optional")]
    public GameObject turret;

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

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Player don't have any selected turret - do not build turret
        if (!buildManager.CanBuild)
            return;

        // Node is already taken by another turret
        if (turret != null)
            return;

        // Build a turret
        buildManager.BuildTurretOnNode(this);

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
