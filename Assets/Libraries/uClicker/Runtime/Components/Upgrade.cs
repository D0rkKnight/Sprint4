using System;
using UnityEngine;

namespace uClicker
{
    [CreateAssetMenu(menuName = "uClicker/Upgrade")]
    public class Upgrade : UnlockableComponent, IGenerableToGObj
    {
        public CurrencyTuple Cost;
        public UpgradePerk[] UpgradePerk;


        // All custy stuff
        public string displayName;
        public Sprite icon;

        public GameObject entryPrefab;
        public string tooltipDescription = "This is a tooltip";

        public GameObject generateEntryObj()
        {
            GameObject entry = Instantiate(entryPrefab);
            ClickerUpgradeButton upgradeBut = entry.GetComponent<ClickerUpgradeButton>();

            upgradeBut.upgrade = this;
            upgradeBut.icon.sprite = icon;

            // Generate the tooltip
            string[] affectedResources = new string[UpgradePerk.Length];
            for (int i = 0; i < UpgradePerk.Length; i++)
            {
                UpgradePerk upgradePerk = UpgradePerk[i];
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

                string amount = BigNumberFormatter.Format(upgradePerk.Amount);

                affectedResources[i] = $"{affectedRsrcLead} {modType} {amount}";
            }

            string tooltip = $"{displayName}\n\n{tooltipDescription}\n\nCost: {BigNumberFormatter.Format(upgradeBut.upgrade.Cost.Amount)}\n\n{string.Join("\n", affectedResources)}";


            upgradeBut.tooltip.TooltipMessage = tooltip;

            return entry;
        }
    }

    [Serializable]
    public enum UpgradeType
    {
        Currency,
        Building,
        Clickable
    }

    [Serializable]
    public class UpgradePerk
    {
        public UpgradeType Type;
        public Building TargetBuilding;
        public Clickable TargetClickable;
        public Currency TargetCurrency;
        public Operation Operation;
        public float Amount;
    }

    public enum Operation
    {
        Add,
        Multiply
    }
}