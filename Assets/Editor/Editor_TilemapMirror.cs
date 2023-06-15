using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TilemapMirror))]
public class Editor_TilemapMirror : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TilemapMirror tilemapMirror = (TilemapMirror)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Mirror Tiles"))
        {
            tilemapMirror.MirrorTiles();
        }
    }
}
