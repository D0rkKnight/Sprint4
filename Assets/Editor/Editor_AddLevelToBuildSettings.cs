using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Editor_AddLevelToBuildSettings : EditorWindow
{

    string scenePath;

    [MenuItem("Tools/Add Level to Build Settings")]
    private static void OpenWindow()
    {
        // Get the currently open scene
        Scene currentScene = SceneManager.GetActiveScene();
        string scenePath = currentScene.path;

        Editor_AddLevelToBuildSettings window = GetWindow<Editor_AddLevelToBuildSettings>();
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Add Level to Build Settings", EditorStyles.boldLabel);

        // Clicking on scenes in the file hierarchy will also select them in the build settings
        string potentialPath = AssetDatabase.GetAssetPath(Selection.activeObject);

        // Confirm is a scene
        if (potentialPath.EndsWith(".unity"))
        {
            scenePath = potentialPath;
        }


        EditorGUILayout.LabelField("Level Path", scenePath);

        if (GUILayout.Button("Add Level"))
        {
            EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;

            bool levelExists = false;
            foreach (EditorBuildSettingsScene scene in buildScenes)
            {
                if (scene.path == scenePath)
                {
                    levelExists = true;
                    break;
                }
            }

            if (!levelExists)
            {
                EditorBuildSettingsScene newScene = new EditorBuildSettingsScene(scenePath, true);
                ArrayUtility.Add(ref buildScenes, newScene);
                EditorBuildSettings.scenes = buildScenes;
                Debug.Log("Level added to build settings!");
            }
            else
            {
                Debug.Log("Level already exists in build settings!");
            }
        }
    }
}
