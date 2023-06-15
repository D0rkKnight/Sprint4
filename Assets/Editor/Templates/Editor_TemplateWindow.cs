using UnityEngine;
using UnityEditor;

public class Editor_TemplateWindow : EditorWindow
{
    [MenuItem("Tools/Templates/My Editor Window")]
    public static void ShowWindow()
    {
        // Show existing window instance. If one doesn't exist, create one.
        Editor_TemplateWindow window = GetWindow<Editor_TemplateWindow>();
        window.Show();
    }

    private void OnGUI()
    {
        // Editor Window GUI code goes here
        EditorGUILayout.LabelField("Hello, Editor Window!");
    }
}
