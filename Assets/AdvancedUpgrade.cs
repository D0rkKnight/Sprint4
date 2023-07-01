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

        // Generate the tooltip
        string[] affectedResources = new string[upgrade.UpgradePerk.Length];
        for (int i = 0; i < upgrade.UpgradePerk.Length; i++) {
            UpgradePerk upgradePerk = upgrade.UpgradePerk[i];
            UpgradeType type = upgradePerk.Type;

            string affectedRsrcName = "";
            switch (type) {
                case UpgradeType.Currency:
                    affectedRsrcName = upgradePerk.TargetCurrency.ToString();
                    break;
                case UpgradeType.Clickable:
                    affectedRsrcName = upgradePerk.TargetClickable.ToString();
                    break;
                case UpgradeType.Building:
                    affectedRsrcName = upgradePerk.TargetBuilding.ToString();
                    break;
            }

            string modType = upgradePerk.Operation.ToString();
            string amount = upgradePerk.Amount.ToString();

            affectedResources[i] = $"{affectedRsrcName} {modType} {amount}";
        }

        string tooltip = $"{displayName}\n\n{tooltipDescription}\n\nCost: {upgradeBut.upgrade.Cost}\n\n{string.Join("\n", affectedResources)}";

        
        upgradeBut.tooltip.TooltipMessage = tooltip;

        return entry;
    }

}

public interface IGenerableToGObj {
    GameObject generateEntryObj();
}