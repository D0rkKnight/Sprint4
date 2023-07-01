using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using uClicker;
using MoreMountains.Tools;
using TMPro;

[RequireComponent(typeof(MMTouchButton))]
public class ClickerBuildingButton : MonoBehaviour
{
    [SerializeField]
    AdvancedBuilding building;
    public TMP_Text buildingNameLabel;
    public TMP_Text buildingCostLabel;
    public TMP_Text buildingCountLabel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buildingNameLabel.text = building.buildingName;
        // buildingCostLabel.text = ClickerManagerComponent.Instance.clickerManager.BuildingCost(building.building).ToString();
        buildingCostLabel.text = BigNumberFormatter.Format(ClickerManagerComponent.Instance.clickerManager.BuildingCost(building.building).Amount);

        int earned = 0;
        if (ClickerManagerComponent.Instance.clickerManager.State.EarnedBuildings.ContainsKey(building.building))
        {
            earned = ClickerManagerComponent.Instance.clickerManager.State.EarnedBuildings[building.building];
        }
        buildingCountLabel.text = earned.ToString();
    }

    public void onButtonPressed()
    {
        if (ClickerManagerComponent.Instance.clickerManager.CanBuy(building.building)) 
        {
            ClickerManagerComponent.Instance.clickerManager.BuyBuilding(building.building);

            Debug.Log("Building purchased");
        }
    }

    public void SetBuilding(AdvancedBuilding building)
    {
        this.building = building;
    }
}
