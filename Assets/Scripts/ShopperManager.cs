using UnityEngine;

public class ShopperManager : MonoBehaviour
{
    public static ShopperManager Instance { get; private set; }

    public GameObject shopperPrefab;
    GameObject shopper;
    int currentWaypoint = 0;

    TrackManager trackManager => TrackManager.Instance;

    void Awake()
    {
        //If Instance does not exist yet, this instance should be the Instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one Singleton in the scene " + transform + " - " + Instance);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shopper = Instantiate(shopperPrefab, trackManager.waypoints[currentWaypoint]);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(shopper.transform.position, trackManager.waypoints[currentWaypoint].position) <= minDistanceToWaypoint)
        //{
        //    shopper.transform.position = trackManager.waypoints[currentWaypoint].position;
        //    currentWaypoint++;
        //    if (currentWaypoint >= trackManager.waypoints.Length)
        //    {
        //        currentWaypoint = 0;
        //    }
        //}
        //else
        //{
        //    shopper.transform.Translate((trackManager.waypoints[currentWaypoint].position - shopper.transform.position).normalized * moveSpeed * Time.deltaTime);
        //}
    }
}
