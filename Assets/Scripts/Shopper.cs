using UnityEngine;

public class Shopper : MonoBehaviour
{
    public float minDistanceToWaypoint = 0.1f;
    public float moveSpeed = 1f;
    int currentWaypoint = 0;

    ShopperManager shopperManager => ShopperManager.Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, shopperManager.trackWaypoints[currentWaypoint].position) <= minDistanceToWaypoint)
        {
            transform.position = shopperManager.trackWaypoints[currentWaypoint].position;
            currentWaypoint++;
            if (currentWaypoint >= shopperManager.trackWaypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        else
        {
            transform.Translate((shopperManager.trackWaypoints[currentWaypoint].position - transform.position).normalized * moveSpeed * Time.deltaTime);
        }
    }
}
