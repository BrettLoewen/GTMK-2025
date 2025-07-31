using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI nameText;

    public void Setup(BuildingData buildingData)
    {
        //image.sprite = buildingData.sprite;
        image.color = buildingData.color;
        nameText.text = buildingData.name;
    }
}
