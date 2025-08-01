using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    public List<BuildingSlot> slots;
    public Building[] buildings;
    public Transform buildingParent;
    public Building buildingPrefab;

    private BuildingData currentBuildingData;

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
        buildings = new Building[slots.Count];

        EnableBuildingSlots(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickedBuildingButton(BuildingData buildingData)
    {
        EnableBuildingSlots(true);
        currentBuildingData = buildingData;
    }

    public void ClickedBuildingSlot(BuildingSlot slot)
    {
        // Disable all slots
        EnableBuildingSlots(false);

        if (currentBuildingData == null)
        {
            return;
        }

        // Spawn the new building
        Building building = Instantiate(buildingPrefab, buildingParent);
        building.transform.position = slot.transform.position;
        building.Setup(currentBuildingData);

        // Store reference to new building at the same index as its slot
        int index = slots.IndexOf(slot);
        buildings[index] = building;

        currentBuildingData = null;
    }

    private void EnableBuildingSlots(bool active)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            // Only enable slot if it isn't already occupied
            if (buildings[i] == null)
            {
                slots[i].gameObject.SetActive(active);
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }
}
