using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
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
        BuildingManager.Instance.ClickedBuildingButton(data);
    }
}
