using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour, ISelectable, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public TextMeshProUGUI nameText;
    private Button button;
    private BuildingData data;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        button.interactable = GameManager.Instance.currentMoney >= data.cost;
    }

    public void Setup(BuildingData buildingData)
    {
        image.sprite = buildingData.sprite;
        //image.color = buildingData.color;
        nameText.text = buildingData.name;

        data = buildingData;
    }

    public void OnClick()
    {
        UIManager.Instance.SetSelectedInfoObject(this);
        BuildingManager.Instance.StartBuildingPlacement(data);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.SetHoveredInfoObject(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.SetHoveredInfoObject(null);
    }

    public string GetInfoText()
    {
        return data.GenerateInfoText();
    }
}
