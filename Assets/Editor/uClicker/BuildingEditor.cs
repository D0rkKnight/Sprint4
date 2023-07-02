using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace uClicker.Editor
{
#if UNITY_EDITOR

    [CustomEditor(typeof(Building))]
    public class BuildingEditor : UnityEditor.Editor
    {
        ClickerComponentEditorUtils editorUtils = new ClickerComponentEditorUtils();

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Building building = (Building)target;

            LanguageMetadata.AutofillLanguageMetaButton(building);

            editorUtils.CreateEditorSprawl(target);

            // Write in building prefab
            if (building.entryPrefab == null)
            {
                building.entryPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Clicker/BuildingEntry.prefab");
            }
        }
    }
#endif
}