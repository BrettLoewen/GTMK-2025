using System.Collections.Generic;
using UnityEngine;

public class ShopperManager : MonoBehaviour
{
    public static ShopperManager Instance { get; private set; }

    public Shopper shopperPrefab;
    Shopper shopper;

    public Transform[] trackWaypoints;

    public float timeBetweenSpawns = 1f;
    private float timeUntilNextSpawn;
    private bool canSpawn = false;

    private int shoppersSpawned = 0;
    private int maxShoppers;
    private int shoppersReturned = 0;

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
        if (canSpawn == false)
        {
            return;
        }

        // Spawn new shoppers every so often
        if (timeUntilNextSpawn <= 0f)
        {
            shopper = Instantiate(shopperPrefab, transform);
            shopper.transform.position = trackWaypoints[0].position;

            shopper.Setup(trackWaypoints, GenerateNeeds());

            timeUntilNextSpawn = timeBetweenSpawns;
            shoppersSpawned++;

            // If all the shoppers for this day have been spawned
            if (shoppersSpawned >= maxShoppers)
            {
                canSpawn = false;
            }
        }
        else
        {
            timeUntilNextSpawn -= Time.deltaTime;
        }
    }

    public void StartDay(int numShoppers)
    {
        canSpawn = true;
        maxShoppers = numShoppers;
        shoppersSpawned = 0;
        shoppersReturned = 0;
        timeUntilNextSpawn = 0f;
    }

    public void ShopperReturned()
    {
        shoppersReturned++;

        if (shoppersReturned >= maxShoppers)
        {
            GameManager.Instance.EndDay();
        }
    }

    private Dictionary<Need, int> GenerateNeeds()
    {
        Dictionary<Need, int> needs = new Dictionary<Need, int>();

        // Don't accidentally act on an invalid day number
        if (GameManager.Instance.currentDay <= 0 || GameManager.Instance.currentDay > GameManager.Instance.scaleNeedDegrees.Length)
        {
            return needs;
        }

        // Get the need degree for the current day number
        int needDegree = GameManager.Instance.scaleNeedDegrees[GameManager.Instance.currentDay - 1];

        int index = 0;
        List<Need> validNeeds = new List<Need>();
        foreach (Need needType in System.Enum.GetValues(typeof(Need)))
        {
            // Only consider needs that are unlocked at the current day number
            if (index <= GameManager.Instance.needUnlockDay[GameManager.Instance.currentDay - 1])
            {
                validNeeds.Add(needType);
            }
            index++;
        }

        // Generate need-degree pairs until the sum of degrees reaches 0
        while (needDegree > 0)
        {
            Need need = validNeeds[Random.Range(0, validNeeds.Count)];
            int degree = Random.Range(1, needDegree);

            if (needs.ContainsKey(need))
            {
                needs[need] += degree;
            }
            else
            {
                needs.Add(need, degree);
            }

            needDegree -= degree;
        }

        return needs;
    }
}
