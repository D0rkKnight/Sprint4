using UnityEngine;

using uClicker;

[CreateAssetMenu(fileName = "AdvancedUpgrade", menuName = "Sprint4/AdvancedUpgrade", order = 0)]
public class AdvancedUpgrade : ScriptableObject, IGenerableToGObj
{

    public Upgrade upgrade;
    public string displayName;
    public Sprite icon;

    public GameObject entryPrefab;

    public string tooltipDescription = "This is a tooltip";

    public GameObject generateEntryObj()
    {
        GameObject entry = Instantiate(entryPrefab);
        ClickerUpgradeButton upgradeBut = entry.GetComponent<ClickerUpgradeButton>();

        upgradeBut.upgrade = upgrade;
        upgradeBut.icon.sprite = icon;

        // Generate the tooltip
        string[] affectedResources = new string[upgrade.UpgradePerk.Length];
        for (int i = 0; i < upgrade.UpgradePerk.Length; i++)
        {
            UpgradePerk upgradePerk = upgrade.UpgradePerk[i];
            UpgradeType type = upgradePerk.Type;

            string affectedRsrcLead = "";
            IHasLanguageMetadata lMeta;
            switch (type)
            {
                case UpgradeType.Currency:
                    affectedRsrcLead = upgradePerk.TargetCurrency.ToString();
                    break;
                case UpgradeType.Clickable:
                    lMeta = upgradePerk.TargetClickable;
                    affectedRsrcLead = lMeta.languageMetadata.getPastActedOn();
                    break;
                case UpgradeType.Building:
                    lMeta = upgradePerk.TargetBuilding;
                    affectedRsrcLead = lMeta.languageMetadata.getPastActedOn();
                    break;
            }

            string modType = "";
            switch (upgradePerk.Operation)
            {
                case Operation.Add:
                    modType = "+";
                    break;
                case Operation.Multiply:
                    modType = "x";
                    break;
            }

            string amount = upgradePerk.Amount.ToString();

            affectedResources[i] = $"{affectedRsrcLead} {modType} {amount}";
        }

        string tooltip = $"{displayName}\n\n{tooltipDescription}\n\nCost: {upgradeBut.upgrade.Cost}\n\n{string.Join("\n", affectedResources)}";


        upgradeBut.tooltip.TooltipMessage = tooltip;

        return entry;
    }

}

public interface IGenerableToGObj
{
    GameObject generateEntryObj();
}