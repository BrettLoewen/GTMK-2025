using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour, ISelectable, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public TextMeshProUGUI nameText;
    private BuildingData data;

    public void Setup(BuildingData buildingData)
    {
        //image.sprite = buildingData.sprite;
        image.color = buildingData.color;
        nameText.text = buildingData.name;

        data = buildingData;
    }

    public void OnClick()
    {
        BuildingManager.Instance.StartBuildingPlacement(data);
        UIManager.Instance.SetSelectedInfoObject(this);
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
