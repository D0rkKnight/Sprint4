using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using uClicker;
using MoreMountains.Tools;
using TMPro;
using TwoPm.TooltipDemo;

[RequireComponent(typeof(MMTouchButton))]
public class ClickerBuildingButton : MonoBehaviour
{
    [SerializeField]
    Building building;
    public TMP_Text buildingNameLabel;
    public TMP_Text buildingCostLabel;
    public TMP_Text buildingCountLabel;
    public Image bg;

    public ReactToPointer tooltip;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        IHasLanguageMetadata buildingMeta = this.building;
        buildingNameLabel.text = buildingMeta.languageMetadata.name;
        // buildingCostLabel.text = ClickerManagerComponent.Instance.clickerManager.BuildingCost(building.building).ToString();
        buildingCostLabel.text = BigNumberFormatter.Format(ClickerManagerComponent.Instance.clickerManager.BuildingCost(building).Amount);

        int earned = 0;
        if (ClickerManagerComponent.Instance.clickerManager.State.EarnedBuildings.ContainsKey(building))
        {
            earned = ClickerManagerComponent.Instance.clickerManager.State.EarnedBuildings[building];
        }
        buildingCountLabel.text = earned.ToString();


        // highlight if can buy
        if (ClickerManagerComponent.Instance.clickerManager.CanBuy(building))
        {
            bg.color = new Color(0.2f, 0.5f, 0.2f);
        }
        else
        {
            bg.color = new Color(0.3f, 0.3f, 0.3f);
        }
    }

    public void onButtonPressed()
    {
        if (ClickerManagerComponent.Instance.clickerManager.CanBuy(building))
        {
            ClickerManagerComponent.Instance.clickerManager.BuyBuilding(building);

            Debug.Log("Building purchased");
        }
    }

    public void SetBuilding(Building building)
    {
        this.building = building;
    }
}
