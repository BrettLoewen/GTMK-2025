using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Info Sidebar")]
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    private ISelectable selectedInfoObject;
    private ISelectable hoveredInfoObject;
    public GameObject sellButton;

    [Header("Menu Sidebar")]
    public Transform buildingButtonParent;
    public BuildingButton buildingButtonPrefab;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI dayText;
    public Slider moneyGoalSlider;
    public TextMeshProUGUI moneyGoalText;

    [Header("Popup")]
    public GameObject popup;
    public GameObject gameEndScreen;
    public TextMeshProUGUI gameEndHeader;
    public TextMeshProUGUI gameEndBody;
    public GameObject creditsScreen;

    private GameManager gameManager => GameManager.Instance;

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

        popup.SetActive(false);
        gameEndScreen.SetActive(false);
        creditsScreen.SetActive(false);
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

        moneyText.text = $"${gameManager.currentMoney}";
        dayText.text = $"Day {gameManager.currentDay} / {gameManager.totalDays}";
        moneyGoalSlider.maxValue = gameManager.moneyGoal;
        moneyGoalSlider.minValue = 0;
        moneyGoalSlider.value = gameManager.earnedMoney;
        moneyGoalText.text = $"${gameManager.earnedMoney} / ${gameManager.moneyGoal}";
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

        gameManager.GainMoney(building.data.cost / 2, false);
        
        BuildingManager.Instance.RemoveBuilding(building);
    }

    public void OnPlayClicked()
    {
        gameManager.StartDay();
    }

    public void OnPauseClicked()
    {

    }

    public void OnPlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowGameEndScreen()
    {
        popup.SetActive(true);
        gameEndScreen.SetActive(true);
        gameEndHeader.text = gameManager.earnedMoney >= gameManager.moneyGoal ? "YOU WIN" : "YOU LOST";
        gameEndBody.text = $"Cash Earned: ${gameManager.earnedMoney}\nSatisfied Shoppers: {gameManager.satisfiedShoppers}\nDissatisfied Shoppers: {gameManager.dissatisfiedShoppers}";
    }

    public void OpenCredits()
    {
        popup.SetActive(true);
        creditsScreen.SetActive(true);
    }

    public void CloseCredits()
    {
        popup.SetActive(false);
        creditsScreen.SetActive(false);
    }
}
