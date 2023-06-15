using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoInspectorTemplate))]
public class Editor_MonoInspectorTemplate : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
