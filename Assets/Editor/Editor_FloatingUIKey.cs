using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloatingUIKey))]
public class Editor_FloatingUIKey : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
