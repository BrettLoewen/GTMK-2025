using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, ISelectable
{
    public Transform[] entryPoints;

    private SpriteRenderer spriteRenderer;
    public BuildingData data;


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

    public bool CanMeetNeed(Dictionary<Need, int> needs)
    {
        foreach (NeedLevel needLevel in data.needsMet)
        {
            if (needs.ContainsKey(needLevel.need) && needLevel.level > 0 && needs[needLevel.need] > 0)
            {
                return true;
            }
        }

        return false;
    }

    public void MeetNeeds(Dictionary<Need, int> needs)
    {
        foreach (NeedLevel needLevel in data.needsMet)
        {
            // If the shopper has this need, reduce the need by the level this building provides.
            // If this reduces the need to 0, ensure it doesn't become negative.
            if (needs.ContainsKey(needLevel.need))
            {
                needs[needLevel.need] -= needLevel.level;

                if (needs[needLevel.need] <= 0)
                {
                    needs[needLevel.need] = 0;
                }
            }
        }

        GameManager.Instance.currentMoney += data.earns;
        GameManager.Instance.earnedMoney += data.earns;
    }
}
