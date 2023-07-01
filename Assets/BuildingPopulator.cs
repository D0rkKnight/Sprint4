using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using uClicker;

public class BuildingPopulator : MonoBehaviour
{

    public BuildingsProfile buildingsProfile;
    public ClickerBuildingButton buildingEntryPrefab;

    public Transform container;

    // Start is called before the first frame update
    void Start()
    {
        // Populate the list of buildings
        for (int i = 0; i < buildingsProfile.buildings.Count; i++) {
            AdvancedBuilding building = buildingsProfile.buildings[i];
            ClickerBuildingButton buildingEntry = Instantiate(buildingEntryPrefab, container);
            buildingEntry.SetBuilding(building);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}