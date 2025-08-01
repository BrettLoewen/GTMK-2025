using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Transform buildingButtonParent;
    public BuildingButton buildingButtonPrefab;

    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    private ISelectable selectedInfoObject;
    private ISelectable hoveredInfoObject;

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

        infoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedInfoObject as Object != null)
        {
            infoPanel.SetActive(true);

            infoText.text = selectedInfoObject.GetInfoText();
        }
        else if (hoveredInfoObject as Object != null)
        {
            infoPanel.SetActive(true);

            infoText.text = hoveredInfoObject.GetInfoText();
        }
        else
        {
            infoPanel.SetActive(false);
        }
    }

    public void SetSelectedInfoObject(ISelectable newInfoObject)
    {
        selectedInfoObject = newInfoObject;
    }

    public void SetHoveredInfoObject(ISelectable newInfoObject)
    {
        hoveredInfoObject = newInfoObject;
    }

    public void OnCancelInfoSelection()
    {
        selectedInfoObject = null;
        hoveredInfoObject = null;

        // In the case where the selected object was the building currently being placed, also stop the building placement
        BuildingManager.Instance.StopBuildingPlacement();
    }
}
