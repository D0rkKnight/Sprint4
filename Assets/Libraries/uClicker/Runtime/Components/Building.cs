using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace uClicker
{
    [CreateAssetMenu(menuName = "uClicker/Building")]
    public class Building : UnlockableComponent, IHasLanguageMetadata, IGenerableToGObj
    {
        public CurrencyTuple Cost;
        public CurrencyTuple YieldAmount;

        [SerializeField]
        private LanguageMetadata _languageMetadata;
        LanguageMetadata IHasLanguageMetadata.languageMetadata { get => _languageMetadata; set => _languageMetadata = value; }

        public GameObject entryPrefab;
        public GameObject generateEntryObj()
        {
            GameObject entry = Instantiate(entryPrefab);
            ClickerBuildingButton buildingBut = entry.GetComponent<ClickerBuildingButton>();

            buildingBut.SetBuilding(this);

            string tooltip = $"Yield per (before multipliers): {BigNumberFormatter.Format(YieldAmount.Amount)}";
            buildingBut.tooltip.TooltipMessage = tooltip;

            return entry;
        }

    }

    [Serializable]
    public struct BuildingTuple
    {
        public Building Building;
        public int Amount;
    }

}



