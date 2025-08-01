using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Shopper : MonoBehaviour, ISelectable
{
    public float minDistanceToWaypoint = 0.1f;
    public float moveSpeed = 1f;

    private LinkedList<Transform> waypoints;

    private List<Building> visitedBuildings = new List<Building>();

    ShopperManager shopperManager => ShopperManager.Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Don't move if there aren't any waypoints
        if (waypoints.Count == 0)
        {
            return;
        }

        // If you are very close to a waypoint, snap to it and move to the next one
        if (Vector3.Distance(transform.position, waypoints.First.Value.position) <= minDistanceToWaypoint)
        {
            // Snap to the waypoint
            transform.position = waypoints.First.Value.position;

            // If there will be more waypoints, remove the first one so the next one will be moved to.
            // If this was the last waypoint, destroy yourself because your route is done.
            if (waypoints.Count > 0)
            {
                waypoints.RemoveFirst();
                if (waypoints.Count == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        // If you haven't reached a waypoint yet, move towards it
        else
        {
            transform.Translate((waypoints.First.Value.position - transform.position).normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform other = collision.transform;
        if (other != null)
        {
            // If this is an entry point, get the relevant building
            Building building = BuildingManager.Instance.GetBuildingByEntryPoint(other);

            // If the building exists and you haven't shopped there yet, then enter the building
            if (building != null && !visitedBuildings.Contains(building))
            {
                // Update the path so you enter and exit the building
                waypoints.AddFirst(other);
                waypoints.AddFirst(building.transform);
                waypoints.AddFirst(other);

                // Store the building so you don't enter the building again
                visitedBuildings.Add(building);
            }
        }
    }

    private void OnMouseEnter()
    {
        UIManager.Instance.SetHoveredInfoObject(this);
    }

    private void OnMouseExit()
    {
        UIManager.Instance.SetHoveredInfoObject(null);
    }

    private void OnMouseDown()
    {
        UIManager.Instance.SetSelectedInfoObject(this);
    }

    public void Setup(Transform[] trackPath)
    {
        waypoints = new LinkedList<Transform>();
        foreach (Transform trackWaypoint in trackPath)
        {
            waypoints.AddLast(trackWaypoint);
        }
    }

    public string GetInfoText()
    {
        return "Shopper";
    }
}
