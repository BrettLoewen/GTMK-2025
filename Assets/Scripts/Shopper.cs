using System.Collections.Generic;
using UnityEngine;

public class Shopper : MonoBehaviour, ISelectable
{
    public float minDistanceToWaypoint = 0.1f;
    public float moveSpeed = 1f;

    private LinkedList<Vector3> waypoints;

    private List<Building> visitedBuildings = new List<Building>();

    private Dictionary<Need, int> needs = new Dictionary<Need, int>();

    public Color[] colors;

    //ShopperManager shopperManager => ShopperManager.Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        SetColor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform other = collision.transform;
        if (other != null)
        {
            // If this is an entry point, get the relevant building
            Building building = BuildingManager.Instance.GetBuildingByEntryPoint(other);

            // If the building exists, you haven't shopped there yet, and it is helpful to you, then enter the building
            if (building != null && !visitedBuildings.Contains(building) && building.CanMeetNeed(needs))
            {
                // Update the path so you enter and exit the building
                waypoints.AddFirst(other.position);
                waypoints.AddFirst(building.transform.position);
                waypoints.AddFirst(other.position);

                // Store the building so you don't enter the building again
                visitedBuildings.Add(building);

                // Update your needs based on the building (also pays for using the building)
                building.MeetNeeds(needs);
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

    private void Move()
    {
        // Don't move if there aren't any waypoints
        if (waypoints.Count == 0)
        {
            return;
        }

        // If you are very close to a waypoint, snap to it and move to the next one
        if (Vector3.Distance(transform.position, waypoints.First.Value) <= minDistanceToWaypoint)
        {
            // Snap to the waypoint
            transform.position = waypoints.First.Value;

            // If there will be more waypoints, remove the first one so the next one will be moved to.
            // If this was the last waypoint, destroy yourself because your route is done.
            if (waypoints.Count > 0)
            {
                waypoints.RemoveFirst();
                if (waypoints.Count == 0)
                {
                    // If the shopper's needs were all met, increase the satisfiedShoppers tracker.
                    // If they were not all met, increase the dissatisfiedShoppers tracker.
                    int remainingNeedLevel = 0;
                    foreach (KeyValuePair<Need, int> needLevel in needs)
                    {
                        remainingNeedLevel += needLevel.Value;
                    }
                    if (remainingNeedLevel > 0)
                    {
                        GameManager.Instance.dissatisfiedShoppers++;
                    }
                    else
                    {
                        GameManager.Instance.satisfiedShoppers++;
                    }

                    ShopperManager.Instance.ShopperReturned();

                    Destroy(gameObject);
                }
            }
        }
        // If you haven't reached a waypoint yet, move towards it
        else
        {
            transform.Translate((waypoints.First.Value - transform.position).normalized * moveSpeed * Time.deltaTime);
        }
    }

    public void Setup(Transform[] trackPath, Dictionary<Need, int> needs)
    {
        waypoints = new LinkedList<Vector3>();
        foreach (Transform trackWaypoint in trackPath)
        {
            waypoints.AddLast(trackWaypoint.position);
        }

        this.needs = needs;

        SetColor();
    }

    // Change the shopper's color to indicate how many needs they have
    private void SetColor()
    {
        int needDegree = 0;
        foreach (KeyValuePair<Need, int> need in needs)
        {
            needDegree += need.Value;
        }
        if (needDegree >= colors.Length)
        {
            needDegree = colors.Length - 1;
        }
        GetComponent<SpriteRenderer>().color = colors[needDegree];
    }

    public string GetInfoText()
    {
        string infoText = "Shopper\nMeets:\n";
        foreach (KeyValuePair<Need, int> needLevel in needs)
        {
            infoText += "- " + needLevel.Value.ToString() + " " + needLevel.Key.ToString() + "\n";
        }
        return infoText;
    }
}
