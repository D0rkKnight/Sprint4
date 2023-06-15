using UnityEditor;
using UnityEngine;
using MoreMountains.CorgiEngine;

public class Editor_FinishLevelIncrementor : EditorWindow
{
    private string newLevelName = "New Level Name";

    [MenuItem("Tools/Change Finish Level Name")]
    private static void OpenWindow()
    {
        Editor_FinishLevelIncrementor window = GetWindow<Editor_FinishLevelIncrementor>();
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Change Finish Level Name", EditorStyles.boldLabel);

        GameObject selectedObject = Selection.activeGameObject;

        FinishLevel finishLevel;
        if (selectedObject != null && (finishLevel = selectedObject.GetComponent<FinishLevel>()) != null)
        {
            EditorGUILayout.LabelField("Current Level Name", finishLevel.LevelName);

            newLevelName = EditorGUILayout.TextField("New Level Name", newLevelName);

            if (GUILayout.Button("Change Name"))
                makeChange(finishLevel, newLevelName);

            // Generate autoincremented version
            // Trim off number at the end of the string
            // Add 1 to the number

            // Get current level name
            string currentLevelName = finishLevel.gameObject.scene.name;
            string incremented = Utils.IncrementSubscript(currentLevelName);

            if (GUILayout.Button($"Set to {incremented}"))
                makeChange(finishLevel, incremented);

            // Get Use Finished Level Name
            incremented = Utils.IncrementSubscript(finishLevel.LevelName);

            if (GUILayout.Button($"Increment to {incremented}"))
                makeChange(finishLevel, incremented);
        }
    }

    private void makeChange(FinishLevel finishLevel, string newLevelName)
    {
        Undo.RecordObject(finishLevel, "Change Level Name");
        finishLevel.LevelName = newLevelName;
        EditorUtility.SetDirty(finishLevel);
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(finishLevel.gameObject.scene);

        Debug.Log("Level Name changed successfully!");
    }
}
