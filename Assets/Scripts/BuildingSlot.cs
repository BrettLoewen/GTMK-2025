using UnityEngine;

public class BuildingSlot : MonoBehaviour
{
    public GameObject[] trackAreas;
    public bool[] enabledSlots;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        enabledSlots = new bool[trackAreas.Length];
        for (int i = 0; i < trackAreas.Length; i++)
        {
            enabledSlots[i] = trackAreas[i].activeSelf;
        }

        EnableAreas(false);
    }

    private void OnMouseEnter()
    {
        EnableAreas(true);
    }

    private void OnMouseExit()
    {
        EnableAreas(false);
    }

    private void OnMouseDown()
    {
        EnableAreas(false);
        BuildingManager.Instance.ClickedBuildingSlot(this);
    }

    void EnableAreas(bool active)
    {
        for (int i = 0; i < trackAreas.Length; i++)
        {
            if (enabledSlots[i])
            {
                trackAreas[i].SetActive(active);
            }
            else
            {
                trackAreas[i].SetActive(false);
            }
        }
    }

    public void ShowBuilding(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
