using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoInspectorTemplate))]
public class Editor_NewScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
