using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Transform buildingButtonParent;
    public BuildingButton buildingButtonPrefab;

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
        foreach (Transform t in buildingButtonParent)
        {
            Destroy(t.gameObject);
        }

        foreach (BuildingData building in DataManager.Instance.buildingData)
        {
            BuildingButton button = Instantiate(buildingButtonPrefab, buildingButtonParent);
            button.Setup(building);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
