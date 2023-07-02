using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

using uClicker;

namespace uClicker
{
    public class ClickerComponentEditorUtils
    {
        public string upgradeCreateLocation = "Assets/Data/Upgrades";
        public int multStep = 10;
        public int numUpgrades = 5;
        public GameObject upgradeEntryPrefab;
        public Sprite icon;

        public void CreateEditorSprawl(UnityEngine.Object target)
        {
            GUILayout.Label("Autogenerate Upgrades", EditorStyles.boldLabel);

            // Presume to be a ClickerComponent
            ClickerComponent clickerComponent = (ClickerComponent)target;

            // Create a button that creates a series of upgrades at different buypoints and effectivenesses
            upgradeCreateLocation = GUILayout.TextField(upgradeCreateLocation);
            multStep = EditorGUILayout.IntField("Mult Step", multStep);

            if (upgradeEntryPrefab == null)
            {
                upgradeEntryPrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Clicker/UpgradeEntry.prefab", typeof(GameObject));
            }
            upgradeEntryPrefab = (GameObject)EditorGUILayout.ObjectField("Upgrade Entry Prefab", upgradeEntryPrefab, typeof(GameObject), false);

            if (icon == null)
            {
                icon = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/AssetPacks/_Heathen Engineering/Assets/UX/Icons/Flat Icons [Free]/Free Flat Arrow 2 S Icon.png", typeof(Sprite));
            }
            icon = (Sprite)EditorGUILayout.ObjectField("Icon", icon, typeof(Sprite), false);

            if (GUILayout.Button("Generate Upgrades"))
            {
                string targetDir = upgradeCreateLocation + "/" + clickerComponent.name;

                if (!AssetDatabase.IsValidFolder(targetDir))
                {
                    AssetDatabase.CreateFolder(upgradeCreateLocation, clickerComponent.name);
                }

                // Create a series of upgrades
                for (int i = 0; i < numUpgrades; i++)
                {
                    int multiplier = 2;

                    // Create the upgrade
                    Upgrade upgrade;

                    // if the asset exists, just use the one that's there
                    string assetName = $"{clickerComponent.name}X{multiplier}_{i}";

                    if (AssetDatabase.LoadAssetAtPath<Upgrade>(targetDir + "/" + assetName + ".asset") != null)
                    {
                        upgrade = AssetDatabase.LoadAssetAtPath<Upgrade>(targetDir + "/" + assetName + ".asset");
                    }
                    else
                    {
                        upgrade = ScriptableObject.CreateInstance<Upgrade>();
                        upgrade.name = assetName;

                        AssetDatabase.CreateAsset(upgrade, targetDir + "/" + assetName + ".asset");
                    }

                    // Get a cost of this component (if it's a building or upgrade)
                    float cost = 0;
                    switch (clickerComponent)
                    {
                        case Building building:
                            cost = building.Cost.Amount;
                            upgrade.Cost.Currency = building.Cost.Currency;
                            break;
                        case Clickable clickable:
                            cost = 50; // Meh
                            upgrade.Cost.Currency = clickable.Currency;
                            break;
                    }

                    // Set the upgrade's values
                    upgrade.Cost.Amount = cost * (float)Math.Pow(multStep, i);

                    // Now determine effect
                    UpgradePerk perk = new UpgradePerk();
                    perk.Operation = Operation.Multiply;

                    switch (clickerComponent)
                    {
                        case Building building:
                            perk.Type = UpgradeType.Building;
                            perk.TargetBuilding = building;
                            break;
                        case Clickable clickable:
                            perk.Type = UpgradeType.Clickable;
                            perk.TargetClickable = clickable;
                            break;
                    }

                    perk.Amount = multiplier; // Keep it simple
                    upgrade.UpgradePerk = new UpgradePerk[] { perk };



                    // Fill in the custy stuff
                    upgrade.displayName = $"{clickerComponent.name} X{multiplier}";
                    upgrade.icon = icon;

                    // Set the prefab
                    upgrade.entryPrefab = upgradeEntryPrefab;

                    // Save the upgrade
                    EditorUtility.SetDirty(upgrade);
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
}