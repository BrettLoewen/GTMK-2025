using UnityEngine;

public class ShopperManager : MonoBehaviour
{
    public static ShopperManager Instance { get; private set; }

    public Shopper shopperPrefab;
    Shopper shopper;

    public Transform[] trackWaypoints;

    public float timeBetweenSpawns = 1f;
    private float timeUntilNextSpawn;

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
        timeUntilNextSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn new shoppers every so often
        if (timeUntilNextSpawn <= 0f)
        {
            shopper = Instantiate(shopperPrefab, transform);
            shopper.transform.position = trackWaypoints[0].position;
            shopper.Setup(trackWaypoints);

            timeUntilNextSpawn = timeBetweenSpawns;
        }
        else
        {
            timeUntilNextSpawn -= Time.deltaTime;
        }
    }
}
