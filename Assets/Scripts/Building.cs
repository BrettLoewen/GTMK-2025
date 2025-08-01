using UnityEngine;

public class Building : MonoBehaviour, ISelectable
{
    public Transform[] entryPoints;

    private SpriteRenderer spriteRenderer;
    private BuildingData data;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        UIManager.Instance.SetHoveredInfoObject(this);
    }

    private void OnMouseExit()
    {
        UIManager.Instance.SetHoveredInfoObject(null);
    }

    private void OnMouseDown()
    {
        UIManager.Instance.SetSelectedInfoObject(this);
    }

    public void Setup(BuildingData data)
    {
        this.data = data;

        spriteRenderer.color = data.color;
        name = data.buildingName;
    }

    public string GetInfoText()
    {
        return data.GenerateInfoText();
    }
}
