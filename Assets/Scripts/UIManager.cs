using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Transform buildingButtonParent;
    public BuildingButton buildingButtonPrefab;

    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    private ISelectable selectedInfoObject;
    private ISelectable hoveredInfoObject;
    public GameObject sellButton;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    public Slider moneyGoalSlider;
    public TextMeshProUGUI moneyGoalText;

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

            sellButton.SetActive(selectedInfoObject as Building != null);
        }
        else if (hoveredInfoObject as Object != null)
        {
            infoPanel.SetActive(true);

            infoText.text = hoveredInfoObject.GetInfoText();

            sellButton.SetActive(hoveredInfoObject as Building != null);
        }
        else
        {
            infoPanel.SetActive(false);
        }

        moneyText.text = $"${GameManager.Instance.currentMoney}";
        dayText.text = $"Day {GameManager.Instance.currentDay} / {GameManager.Instance.totalDays}";
        moneyGoalSlider.maxValue = GameManager.Instance.moneyGoal;
        moneyGoalSlider.minValue = 0;
        moneyGoalSlider.value = GameManager.Instance.earnedMoney;
        moneyGoalText.text = $"${GameManager.Instance.earnedMoney} / ${GameManager.Instance.moneyGoal}";
    }

    public void SetSelectedInfoObject(ISelectable newInfoObject)
    {
        if (selectedInfoObject != null)
        {
            OnCancelInfoSelection();
        }

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

    public void OnSellClicked()
    {
        Building building = null;
        if (selectedInfoObject != null)
        {
            building = selectedInfoObject as Building;
        }
        else if (hoveredInfoObject != null)
        {
            building = hoveredInfoObject as Building;
        }

        if (building == null)
        {
            return;
        }

        GameManager.Instance.currentMoney += building.data.cost / 2;
        
        BuildingManager.Instance.RemoveBuilding(building);
    }

    public void OnPlayClicked()
    {
        GameManager.Instance.StartDay();
    }

    public void OnPauseClicked()
    {

    }
}
