using UnityEngine;
using UnityEditor;
using System.IO;

public class Editor_CreateEditorScript : EditorWindow
{
    string newScriptName = "Editor_NewScript";
    int selectedTemplateIndex = 0;

    Object script;


    [MenuItem("Tools/Template Script Creator")]
    public static void ShowWindow()
    {
        // Show existing window instance. If one doesn't exist, create one.
        EditorWindow.GetWindow(typeof(Editor_CreateEditorScript));
    }

    private void OnGUI()
    {
        GUILayout.Label("Template Script Creator", EditorStyles.boldLabel);
        newScriptName = EditorGUILayout.TextField("New Script Name", newScriptName);

        // Create a dropdown out of the contents of Editor/Templates
        string[] templateScriptNames = Directory.GetFiles(Path.Combine(Application.dataPath, "Editor", "Templates"), "*.cs");
        for (int i = 0; i < templateScriptNames.Length; i++)
        {
            templateScriptNames[i] = Path.GetFileNameWithoutExtension(templateScriptNames[i]);
        }

        selectedTemplateIndex = EditorGUILayout.Popup("Template Script", selectedTemplateIndex, templateScriptNames);
        string selectedTemplateScriptName = templateScriptNames[selectedTemplateIndex];

        if (GUILayout.Button("Create Template Script"))
        {
            string templateScriptContent = GetTemplateScriptContent(selectedTemplateScriptName, newScriptName, "Editor");
            CreateTemplateScript("Editor", newScriptName, templateScriptContent);
        }


        // Make some space
        EditorGUILayout.Space();



        // Create an Editor script for an existing MonoBehaviour script
        GUILayout.Label("Create Editor Script for MonoBehaviour", EditorStyles.boldLabel);

        // Create a drag field for a MonoBehaviour script FILE
        script = EditorGUILayout.ObjectField("MonoBehaviour Script", script, typeof(UnityEngine.Object), false);

        // Get asset path
        string assetPath = AssetDatabase.GetAssetPath(script);

        // Get asset name (trim off path and .cs)
        string assetName = Path.GetFileNameWithoutExtension(assetPath);

        if (script != null)
        {
            // Create a button to create an Editor script for the MonoBehaviour script
            if (GUILayout.Button("Create Editor Script"))
            {
                string newScriptName_tmp = "Editor_" + assetName;
                string templateScriptContent = GetTemplateScriptContent("Editor_MonoInspectorTemplate", newScriptName_tmp, "Editor");

                // Replace [CustomEditor(typeof(*))] with the name of the MonoBehaviour script
                templateScriptContent = templateScriptContent.Replace("MonoInspectorTemplate", assetName); // Sure this works for now

                CreateTemplateScript("Editor", newScriptName_tmp, templateScriptContent);
            }
        }
    }

    private void CreateTemplateScript(string folderName, string newScriptName, string content)
    {
        string scriptPath = Path.Combine(Application.dataPath, folderName, newScriptName + ".cs");
        File.WriteAllText(scriptPath, content);
        AssetDatabase.Refresh();

        Debug.Log("Template script created at: " + scriptPath);
    }

    private string GetTemplateScriptContent(string templateName, string newScriptName, string folderName)
    {
        // Read from file
        string templateScriptPath = Path.Combine(Application.dataPath, folderName, "Templates", templateName + ".cs");
        string templateScriptContent = File.ReadAllText(templateScriptPath);

        // Replace all instances of "Editor_TemplateWindow" with the name of the script
        templateScriptContent = templateScriptContent.Replace(templateName, newScriptName);

        return templateScriptContent;
    }
}
