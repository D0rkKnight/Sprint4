using UnityEngine;

using uClicker;

[CreateAssetMenu(fileName = "AdvancedUpgrade", menuName = "Sprint4/AdvancedUpgrade", order = 0)]
public class AdvancedUpgrade : ScriptableObject, IGenerableToGObj {
    
    public Upgrade upgrade;
    public string displayName;
    public Sprite icon;

    public GameObject entryPrefab;

    public string tooltipDescription = "This is a tooltip";

    public GameObject generateEntryObj() {
        GameObject entry = Instantiate(entryPrefab);
        ClickerUpgradeButton upgradeBut = entry.GetComponent<ClickerUpgradeButton>();
        
        upgradeBut.upgrade = upgrade;
        upgradeBut.icon.sprite = icon;
        upgradeBut.tooltip.TooltipMessage = tooltipDescription;
        
        return entry;
    }

}

public interface IGenerableToGObj {
    GameObject generateEntryObj();
}