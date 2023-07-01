using UnityEditor;
using UnityEngine;

using uClicker;

// Editor window
public class Editor_CreateAdvancedWrapper : EditorWindow
{
    // Profile
    BuildingsProfile profile;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Tools/Create Buildings Wrapper")]
    public static void ShowWindow()
    {
        // Show existing window instance. If one doesn't exist, make one.
        Editor_CreateAdvancedWrapper window = (Editor_CreateAdvancedWrapper)EditorWindow.GetWindow(typeof(Editor_CreateAdvancedWrapper));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Create Advanced Wrapper", EditorStyles.boldLabel);

        if (GUILayout.Button("Create Wrapper"))
        {
            // Take the currently selected Buildings SO and create advanced version
            Building profile = (Building)Selection.activeObject;

            // Create a new SO
            AdvancedBuilding wrapper = ScriptableObject.CreateInstance<AdvancedBuilding>();

            // Copy the values
            wrapper.building = profile;
            wrapper.buildingName = profile.name;

            // Save the new SO
            string path = AssetDatabase.GetAssetPath(profile);
            path = path.Replace(".asset", "_Advanced.asset");
            AssetDatabase.CreateAsset(wrapper, path);
            AssetDatabase.SaveAssets();
        }
    }
}