using UnityEngine;

public class Shopper : MonoBehaviour
{
    public float minDistanceToWaypoint = 0.1f;
    public float moveSpeed = 1f;
    int currentWaypoint = 0;

    TrackManager trackManager => TrackManager.Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, trackManager.waypoints[currentWaypoint].position) <= minDistanceToWaypoint)
        {
            transform.position = trackManager.waypoints[currentWaypoint].position;
            currentWaypoint++;
            if (currentWaypoint >= trackManager.waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        else
        {
            transform.Translate((trackManager.waypoints[currentWaypoint].position - transform.position).normalized * moveSpeed * Time.deltaTime);
        }
    }
}
